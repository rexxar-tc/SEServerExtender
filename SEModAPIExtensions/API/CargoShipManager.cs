﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Sandbox.Common.ObjectBuilders;

using SEModAPI.API.Definitions;

using SEModAPIInternal.API.Common;
using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Utility;
using SEModAPIInternal.Support;

using VRageMath;
using VRage.Common.Utils;

namespace SEModAPIExtensions.API
{
	public class CargoShipManager
	{
		private static CargoShipManager m_instance;

		protected CargoShipManager()
		{
			m_instance = this;

			Console.WriteLine("Finished loading CargoShipManager");
		}

		public static CargoShipManager Instance
		{
			get
			{
				if (m_instance == null)
					m_instance = new CargoShipManager();

				return m_instance;
			}
		}

		public void SpawnCargoShipGroup(ulong remoteUserId = 0)
		{
			try
			{
				//Load the spawn groups
				SpawnGroupsDefinitionsManager spawnGroupsDefinitionsManager = new SpawnGroupsDefinitionsManager();
				FileInfo contentDataFile = new FileInfo(Path.Combine(MyFileSystem.ContentPath, "Data", "SpawnGroups.sbc"));
				spawnGroupsDefinitionsManager.Load(contentDataFile);

				//Calculate lowest and highest frequencies
				float lowestFrequency = 999999;
				float highestFrequency = 0;
				foreach (SpawnGroupDefinition entry in spawnGroupsDefinitionsManager.Definitions)
				{
					if (entry.Frequency < lowestFrequency)
						lowestFrequency = entry.Frequency;
					if (entry.Frequency > highestFrequency)
						highestFrequency = entry.Frequency;
				}
				if (lowestFrequency <= 0)
					lowestFrequency = 1;

				//Get a list of which groups *could* spawn
				Random random = new Random((int)DateTime.Now.ToBinary());
				double randomChance = random.NextDouble();
				randomChance = randomChance * (highestFrequency / lowestFrequency);
				List<SpawnGroupDefinition> possibleGroups = new List<SpawnGroupDefinition>();
				foreach (SpawnGroupDefinition entry in spawnGroupsDefinitionsManager.Definitions)
				{
					if (entry.Frequency >= randomChance)
					{
						possibleGroups.Add(entry);
					}
				}

				//Determine which group *will* spawn
				randomChance = random.NextDouble();
				int randomShipIndex = Math.Min((int)Math.Round(randomChance * possibleGroups.Count, 0), possibleGroups.Count);
				SpawnGroupDefinition randomSpawnGroup = possibleGroups[randomShipIndex];

				ChatManager.Instance.SendPrivateChatMessage(remoteUserId, "Spawning cargo group '" + randomSpawnGroup.Name + "' ...");

				//Spawn the ships in the group
				float worldSize = SandboxGameAssemblyWrapper.Instance.GetServerConfig().SessionSettings.WorldSizeKm * 1000.0f;
				float spawnSize = 0.5f * worldSize;
				float destinationSize = 0.01f * spawnSize;
				Vector3 groupPosition = UtilityFunctions.GenerateRandomBorderPosition(new Vector3(-spawnSize, -spawnSize, -spawnSize), new Vector3(spawnSize, spawnSize, spawnSize));
				Vector3 destinationPosition = UtilityFunctions.GenerateRandomBorderPosition(new Vector3(-destinationSize, -destinationSize, -destinationSize), new Vector3(destinationSize, destinationSize, destinationSize));
				Matrix orientation = Matrix.CreateLookAt(groupPosition, destinationPosition, new Vector3(0, 1, 0));
				foreach (SpawnGroupPrefab entry in randomSpawnGroup.Prefabs)
				{
					FileInfo prefabFile = new FileInfo(Path.Combine(MyFileSystem.ContentPath, "Data", "Prefabs", entry.File));
					if (!prefabFile.Exists)
						continue;

					//Create the ship
					CubeGridEntity cubeGrid = new CubeGridEntity(prefabFile);

					//Set the ship position and orientation
					Vector3 shipPosition = Vector3.Transform(entry.Position, orientation) + groupPosition;
					orientation.Translation = shipPosition;
					MyPositionAndOrientation newPositionOrientation = new MyPositionAndOrientation(orientation);
					cubeGrid.PositionAndOrientation = newPositionOrientation;

					//Set the ship velocity
					//Speed is clamped between 1.0f and the max cube grid speed
					Vector3 travelVector = destinationPosition - groupPosition;
					travelVector.Normalize();
					Vector3 shipVelocity = travelVector * (float)Math.Min(cubeGrid.MaxLinearVelocity, Math.Max(1.0f, entry.Speed));
					cubeGrid.LinearVelocity = shipVelocity;

					//And add the ship to the world
					SectorObjectManager.Instance.AddEntity(cubeGrid);
				}

				ChatManager.Instance.SendPrivateChatMessage(remoteUserId, "Cargo group '" + randomSpawnGroup.Name + "' spawned with " + randomSpawnGroup.Prefabs.Length.ToString() + " ships at " + groupPosition.ToString());
			}
			catch (Exception ex)
			{
				LogManager.GameLog.WriteLine(ex);
			}
		}
	}
}
