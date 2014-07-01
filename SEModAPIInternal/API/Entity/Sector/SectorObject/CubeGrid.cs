﻿using Microsoft.Xml.Serialization.GeneratedAssembly;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Common.ObjectBuilders.VRageData;

using SEModAPI.API;
using SEModAPI.API.Definitions;

using SEModAPIInternal.API.Common;
using SEModAPIInternal.API.Server;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;
using SEModAPIInternal.API.Utility;
using SEModAPIInternal.Support;

using VRageMath;

namespace SEModAPIInternal.API.Entity.Sector.SectorObject
{
	public class CubeGridEntity : BaseEntity
	{
		#region "Attributes"

		private CubeBlockManager m_cubeBlockManager;

		public static string CubeGridNamespace = "5BCAC68007431E61367F5B2CF24E2D6F";
		public static string CubeGridClass = "98262C3F38A1199E47F2B9338045794C";

		public static string CubeGridSetDampenersEnabledMethod = "86B66668D555E1C1B744C17D2AFA77F7";
		public static string CubeGridGetCubeBlocksHashSetMethod = "E38F3E9D7A76CD246B99F6AE91CC3E4A";

		public static string CubeGridIsStaticField = "";
		public static string CubeGridBlockGroupsField = "24E0633A3442A1F605F37D69F241C970";

		#endregion

		#region "Constructors and Initializers"

		public CubeGridEntity(FileInfo prefabFile)
			: base(null)
		{
			BaseEntity = BaseEntityManager.LoadContentFile<MyObjectBuilder_CubeGrid, MyObjectBuilder_CubeGridSerializer>(prefabFile);

			m_cubeBlockManager = new CubeBlockManager();
			List<CubeBlockEntity> cubeBlockList = new List<CubeBlockEntity>();
			foreach(var cubeBlock in GetSubTypeEntity().CubeBlocks)
			{
				cubeBlockList.Add(new CubeBlockEntity(cubeBlock));
			}
			m_cubeBlockManager.Load(cubeBlockList.ToArray());
		}

		public CubeGridEntity(MyObjectBuilder_CubeGrid definition)
			: base(definition)
		{
			m_cubeBlockManager = new CubeBlockManager();
			List<CubeBlockEntity> cubeBlockList = new List<CubeBlockEntity>();
			foreach(var cubeBlock in GetSubTypeEntity().CubeBlocks)
			{
				switch (cubeBlock.TypeId)
				{
					case MyObjectBuilderTypeEnum.CargoContainer:
						cubeBlockList.Add(new CargoContainerEntity((MyObjectBuilder_CargoContainer)cubeBlock));
						break;
					case MyObjectBuilderTypeEnum.Reactor:
						cubeBlockList.Add(new ReactorEntity((MyObjectBuilder_Reactor)cubeBlock));
						break;
					case MyObjectBuilderTypeEnum.MedicalRoom:
						cubeBlockList.Add(new MedicalRoomEntity((MyObjectBuilder_MedicalRoom)cubeBlock));
						break;
					case MyObjectBuilderTypeEnum.Cockpit:
						cubeBlockList.Add(new CockpitEntity((MyObjectBuilder_Cockpit)cubeBlock));
						break;
					case MyObjectBuilderTypeEnum.Beacon:
						cubeBlockList.Add(new BeaconEntity((MyObjectBuilder_Beacon)cubeBlock));
						break;
					case MyObjectBuilderTypeEnum.GravityGenerator:
						cubeBlockList.Add(new GravityGeneratorEntity((MyObjectBuilder_GravityGenerator)cubeBlock));
						break;
					default:
						cubeBlockList.Add(new CubeBlockEntity(cubeBlock));
						break;
				}
			}
			m_cubeBlockManager.Load(cubeBlockList);
		}

		public CubeGridEntity(MyObjectBuilder_CubeGrid definition, Object backingObject)
			: base(definition, backingObject)
		{
			m_cubeBlockManager = new CubeBlockManager(backingObject, CubeGridGetCubeBlocksHashSetMethod);
		}

		#endregion

		#region "Properties"

		[Category("Cube Grid")]
		[ReadOnly(true)]
		public override string Name
		{
			get
			{
				if (GetSubTypeEntity() == null)
					return "";

				string name = "";
				foreach (var cubeBlock in GetSubTypeEntity().CubeBlocks)
				{
					if (cubeBlock.TypeId == MyObjectBuilderTypeEnum.Beacon)
					{
						if (name.Length > 0)
							name += "|";
						name += ((MyObjectBuilder_Beacon)cubeBlock).CustomName;
					}
				}
				if (name.Length == 0)
					return GetSubTypeEntity().EntityId.ToString();
				else
					return name;
			}
		}

		[Category("Cube Grid")]
		[ReadOnly(true)]
		public MyCubeSize GridSizeEnum
		{
			get { return GetSubTypeEntity().GridSizeEnum; }
			set
			{
				if (GetSubTypeEntity().GridSizeEnum == value) return;
				GetSubTypeEntity().GridSizeEnum = value;
				Changed = true;
			}
		}

		[Category("Cube Grid")]
		[ReadOnly(true)]
		public bool IsStatic
		{
			get { return GetSubTypeEntity().IsStatic; }
			set
			{
				if (GetSubTypeEntity().IsStatic == value) return;
				GetSubTypeEntity().IsStatic = value;
				Changed = true;
			}
		}

		[Category("Cube Grid")]
		[TypeConverter(typeof(Vector3TypeConverter))]
		public SerializableVector3 LinearVelocity
		{
			get { return GetSubTypeEntity().LinearVelocity; }
			set
			{
				if (GetSubTypeEntity().LinearVelocity.Equals(value)) return;
				GetSubTypeEntity().LinearVelocity = value;
				Changed = true;

				if (BackingObject != null)
					BaseEntityManagerWrapper.GetInstance().UpdateEntityVelocity(BackingObject, value);
			}
		}

		[Category("Cube Grid")]
		[TypeConverter(typeof(Vector3TypeConverter))]
		public SerializableVector3 AngularVelocity
		{
			get { return GetSubTypeEntity().AngularVelocity; }
			set
			{
				if (GetSubTypeEntity().AngularVelocity.Equals(value)) return;
				GetSubTypeEntity().AngularVelocity = value;
				Changed = true;

				if (BackingObject != null)
					BaseEntityManagerWrapper.GetInstance().UpdateEntityAngularVelocity(BackingObject, value);
			}
		}

		[Category("Cube Grid")]
		[Browsable(false)]
		public List<CubeBlockEntity> CubeBlocks
		{
			get { return m_cubeBlockManager.GetTypedInternalData<CubeBlockEntity>(); }
		}

		[Category("Cube Grid")]
		[Browsable(false)]
		public List<BoneInfo> Skeleton
		{
			get { return GetSubTypeEntity().Skeleton; }
		}

		[Category("Cube Grid")]
		[Browsable(false)]
		public List<MyObjectBuilder_ConveyorLine> ConveyorLines
		{
			get { return GetSubTypeEntity().ConveyorLines; }
		}

		[Category("Cube Grid")]
		[Browsable(false)]
		public List<MyObjectBuilder_BlockGroup> BlockGroups
		{
			get { return GetSubTypeEntity().BlockGroups; }
		}

		#endregion

		#region "Methods"

		/// <summary>
		/// Method to get the casted instance from parent signature
		/// </summary>
		/// <returns>The casted instance into the class type</returns>
		new public MyObjectBuilder_CubeGrid GetSubTypeEntity()
		{
			RefreshCubeBlocks();

			return (MyObjectBuilder_CubeGrid)BaseEntity;
		}

		public void RefreshCubeBlocks()
		{
			MyObjectBuilder_CubeGrid cubeGrid = (MyObjectBuilder_CubeGrid)BaseEntity;

			//Refresh the cube blocks content in the cube grid from the cube blocks manager
			cubeGrid.CubeBlocks.Clear();
			foreach (var item in m_cubeBlockManager.Definitions)
			{
				cubeGrid.CubeBlocks.Add((MyObjectBuilder_CubeBlock)item.GetSubTypeEntity());
			}
		}

		public void Export(FileInfo fileInfo)
		{
			BaseEntityManager.SaveContentFile<MyObjectBuilder_CubeGrid, MyObjectBuilder_CubeGridSerializer>(GetSubTypeEntity(), fileInfo);
		}

		public bool AddCubeGrid()
		{
			try
			{
				Action action = InternalAddCubeGrid;
				SandboxGameAssemblyWrapper.EnqueueMainGameAction(action);

				return true;
			}
			catch (Exception ex)
			{
				LogManager.GameLog.WriteLine(ex);
				return false;
			}
		}

		public void InternalAddCubeGrid()
		{
			try
			{
				if (SandboxGameAssemblyWrapper.IsDebugging)
					Console.WriteLine("CubeGrid '" + Name + "' is being added ...");

				Type backingType = SandboxGameAssemblyWrapper.GetInstance().GameAssembly.GetType(CubeGridNamespace + "." + CubeGridClass);

				//Create a blank instance of the base type
				BackingObject = Activator.CreateInstance(backingType);

				//Invoke 'Init' using the sub object of the grid which is the MyObjectBuilder_CubeGrid type
				MethodInfo initMethod = BackingObject.GetType().GetMethod("Init", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				initMethod.Invoke(BackingObject, new object[] { GetSubTypeEntity() });

				//Add the entity to the scene
				BaseEntityManagerWrapper.GetInstance().AddEntity(BackingObject);
			}
			catch (Exception ex)
			{
				LogManager.GameLog.WriteLine(ex);
			}
		}

		#endregion
	}

	public class CubeGridManager : BaseEntityManager
	{
		#region "Methods"

		new protected bool GetChangedState(CubeGridEntity overLayer)
		{
			foreach (var def in overLayer.CubeBlocks)
			{
				CubeBlockEntity cubeBlock = (CubeBlockEntity)def;
				if (cubeBlock.Changed)
					return true;
			}

			return overLayer.Changed;
		}

		#endregion
	}
}
