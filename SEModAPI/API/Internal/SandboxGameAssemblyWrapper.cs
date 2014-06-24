﻿using SteamSDK;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;

using SysUtils.Utils;

using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Common.ObjectBuilders.VRageData;

using VRage;
using VRageMath;

namespace SEModAPI.API.Internal
{
	public class SandboxGameAssemblyWrapper : BaseInternalWrapper
	{
		#region "Attributes"

		protected new static SandboxGameAssemblyWrapper m_instance;

		private static Assembly m_assembly;

		private static Type m_mainGameType;
		private Type m_checkpointManagerType;
		private Type m_serverCoreType;
		private static Type m_configContainerType;
		private Type m_stringLookupType1;

		private MethodInfo m_saveCheckpoint;
		private MethodInfo m_getServerSector;
		private MethodInfo m_getServerCheckpoint;
		private static MethodInfo m_setConfigWorldName;
		private MethodInfo m_stringLookupMethod1;

		private FieldInfo m_mainGameInstanceField;
		private static FieldInfo m_configContainerField;
		private static FieldInfo m_configContainerDedicatedDataField;
		private static FieldInfo m_serverCoreNullRender;

		public static string MainGameClass = "B337879D0C82A5F9C44D51D954769590.B3531963E948FB4FA1D057C4340C61B4";

		public static string MainGameAction1 = "0CAB22C866086930782A91BA5F21A936";	//() Entity loading complete
		public static string MainGameAction2 = "736ABFDB88EC08BFEA24D3A2AB06BE80";	//(Bool) ??
		public static string MainGameAction3 = "F7E4614DB0033215C446B502BA17BDDB";	//() Triggers Action1
		public static string MainGameAction4 = "B43682C38AD089E0EE792C74E4503633";	//() Triggered by 'Ctrl+C'

		#endregion

		#region "Constructors and Initializers"

		protected SandboxGameAssemblyWrapper(string path)
			: base(path)
		{
			m_instance = this;

			//string assemblyPath = Path.Combine(path, "Sandbox.Game.dll");
			m_assembly = Assembly.UnsafeLoadFrom("Sandbox.Game.dll");
			ResourceManager resourceManager = new ResourceManager("Resources.Strings", m_assembly);

			//string conveyorLineManagerClass = "8EAF60352312606996BD8147B0A3C880.68E5FDFBB1457F6347DEBE26175326B0";
			//string MySandboxGame_ExitMethod = "246E732EE67F7F6F88C4FF63B3901107";
			//string MySandboxGame_Initialize = "2AA66FBD3F2C5EC250558B3136F3974A";

			m_mainGameType = m_assembly.GetType(MainGameClass);
			m_checkpointManagerType = m_assembly.GetType("36CC7CE820B9BBBE4B3FECFEEFE4AE86.828574590CB1B1AE5A5659D4B9659548");
			m_serverCoreType = m_assembly.GetType("168638249D29224100DB50BB468E7C07.7BAD4AFD06B91BCD63EA57F7C0D4F408");
			m_stringLookupType1 = m_assembly.GetType("B337879D0C82A5F9C44D51D954769590.2F60967103E6024E563836A2572899F1");

			Type[] argTypes = new Type[2];
			argTypes[0] = typeof(MyObjectBuilder_Checkpoint);
			argTypes[1] = typeof(string);
			m_saveCheckpoint = m_checkpointManagerType.GetMethod("03AA898C5E9A48425F2CB4FFB2A49A82", argTypes);
			m_getServerSector = m_checkpointManagerType.GetMethod("B6D13C37B0C7FDBF469AB93D18E4444A", BindingFlags.Static | BindingFlags.Public);
			m_getServerCheckpoint = m_checkpointManagerType.GetMethod("825106F82475488A49F8184C627DADEB", BindingFlags.Static | BindingFlags.Public);
			m_stringLookupMethod1 = m_stringLookupType1.GetMethod("D893F76B8F00FC565CF848A64C4B6F97", BindingFlags.Static | BindingFlags.NonPublic);

			m_mainGameInstanceField = m_mainGameType.GetField("392503BDB6F8C1E34A232489E2A0C6D4", BindingFlags.Static | BindingFlags.Public);
			m_configContainerField = m_mainGameType.GetField("4895ADD02F2C27ED00C63E7E506EE808", BindingFlags.Static | BindingFlags.Public);
			m_configContainerType = m_configContainerField.FieldType;
			m_configContainerDedicatedDataField = m_configContainerType.GetField("44A1510B70FC1BBE3664969D47820439", BindingFlags.NonPublic | BindingFlags.Instance);
			m_serverCoreNullRender = m_serverCoreType.GetField("53A34747D8E8EDA65E601C194BECE141", BindingFlags.Public | BindingFlags.Static);

			m_setConfigWorldName = m_configContainerType.GetMethod("493E0E7BC7A617699C44A9A5FB8FF679", BindingFlags.Public | BindingFlags.Instance);

			Console.WriteLine("Finished loading Sandbox.Game.dll assembly wrapper");
		}

		new public static SandboxGameAssemblyWrapper GetInstance(string basePath = "")
		{
			if (m_instance == null)
			{
				m_instance = new SandboxGameAssemblyWrapper(basePath);
			}
			return (SandboxGameAssemblyWrapper)m_instance;
		}

		#endregion

		#region "Properties"

		public Type MainGameType
		{
			get { return m_mainGameType; }
		}

		public Type CheckpointManagerType
		{
			get { return m_checkpointManagerType; }
		}

		new public static bool IsDebugging
		{
			get
			{
				SandboxGameAssemblyWrapper.GetInstance();
				return m_isDebugging;
			}
			set
			{
				SandboxGameAssemblyWrapper.GetInstance();
				m_isDebugging = value;
			}
		}

		#endregion

		#region "Methods"

		public static bool SetupMainGameEventHandlers(B337879D0C82A5F9C44D51D954769590.B3531963E948FB4FA1D057C4340C61B4 mainGame)
		{
			try
			{
				FieldInfo actionField = m_mainGameType.GetField(MainGameAction1, BindingFlags.NonPublic | BindingFlags.Static);
				Action newAction1 = MainGameEvent1;
				actionField.SetValue(null, newAction1);
				
				actionField = m_mainGameType.GetField(MainGameAction2, BindingFlags.NonPublic | BindingFlags.Instance);
				Action<bool> newAction2 = MainGameEvent2;
				actionField.SetValue(mainGame, newAction2);
				
				actionField = m_mainGameType.GetField(MainGameAction3, BindingFlags.NonPublic | BindingFlags.Static);
				Action newAction3 = MainGameEvent3;
				actionField.SetValue(null, newAction3);
				
				actionField = m_mainGameType.GetField(MainGameAction4, BindingFlags.NonPublic | BindingFlags.Static);
				Action newAction4 = MainGameEvent4;
				actionField.SetValue(null, newAction4);
				
				return true;
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool EnqueueMainGameAction(Action action)
		{
			try
			{
				MethodInfo enqueue = m_mainGameType.GetMethod("0172226C0BA7DAE0B1FCE0AF8BC7F735");
				enqueue.Invoke(GetMainGameInstance(), new object[] { action });

				return true;
			}
			catch (Exception ex)
			{
				//TODO - Find the best way to handle this exception
				return false;
			}
		}

		#region "Actions"

		public static void MainGameEvent1()
		{
			try
			{
				Console.WriteLine("MainGameEvent - Entity loading complete");

				TestSteamAPI();

				FieldInfo actionField = m_mainGameType.GetField(MainGameAction1, BindingFlags.NonPublic | BindingFlags.Static);
				Action newAction = MainGameEvent1;
				actionField.SetValue(null, newAction);
			}
			catch (Exception ex)
			{
				//TODO - Find the best way to handle this exception
				return;
			}
		}

		public static void MainGameEvent2(bool param0)
		{
			try
			{
				Console.WriteLine("MainGameEvent - '2' - " + param0.ToString());

				FieldInfo actionField = m_mainGameType.GetField(MainGameAction2, BindingFlags.NonPublic | BindingFlags.Instance);
				Action<bool> newAction = MainGameEvent2;
				actionField.SetValue(GetMainGameInstance(), newAction);
			}
			catch (Exception ex)
			{
				//TODO - Find the best way to handle this exception
				return;
			}
		}

		public static void MainGameEvent3()
		{
			try
			{
				Console.WriteLine("MainGameEvent - '3'");

				FieldInfo actionField = m_mainGameType.GetField(MainGameAction3, BindingFlags.NonPublic | BindingFlags.Static);
				Action newAction = MainGameEvent3;
				actionField.SetValue(null, newAction);
			}
			catch (Exception ex)
			{
				//TODO - Find the best way to handle this exception
				return;
			}
		}

		public static void MainGameEvent4()
		{
			try
			{
				Console.WriteLine("MainGameEvent - 'Ctrl+C' pressed");

				FieldInfo actionField = m_mainGameType.GetField(MainGameAction4, BindingFlags.NonPublic | BindingFlags.Static);
				Action newAction = MainGameEvent4;
				actionField.SetValue(null, newAction);
			}
			catch (Exception ex)
			{
				//TODO - Find the best way to handle this exception
				return;
			}
		}

		#endregion

		private string GetLookupString(MethodInfo method, int key, int start, int length)
		{
			string result = (string)method.Invoke(null, new object[] { key, start, length });
			return result;
		}

		public string GetLookupString1(int key, int start, int length)
		{
			return GetLookupString(m_stringLookupMethod1, key, start, length);
		}

		public static Object GetServerConfigContainer()
		{
			Object configObject = m_configContainerField.GetValue(null);

			return configObject;
		}

		public static MyConfigDedicatedData GetServerConfig()
		{
			Object configContainer = GetServerConfigContainer();
			MyConfigDedicatedData config = (MyConfigDedicatedData)m_configContainerDedicatedDataField.GetValue(configContainer);
			if (config == null)
			{
				MethodInfo loadConfigDataMethod = m_configContainerField.FieldType.GetMethod("4DD64FD1D45E514D01C925D07B69B3BE", BindingFlags.Public | BindingFlags.Instance);
				loadConfigDataMethod.Invoke(configContainer, new object[] { });
				config = (MyConfigDedicatedData)m_configContainerDedicatedDataField.GetValue(configContainer);
			}

			return config;
		}

		public bool SaveCheckpoint(MyObjectBuilder_Checkpoint checkpoint, string worldName)
		{
			return (bool)m_saveCheckpoint.Invoke(null, new object[] { checkpoint, worldName });
		}

		public MyObjectBuilder_Sector GetServerSector(string worldName, Vector3I sectorLocation, out ulong sectorId)
		{
			object[] parameters = new object[] { worldName, sectorLocation, null };
			MyObjectBuilder_Sector result = (MyObjectBuilder_Sector)m_getServerSector.Invoke(null, parameters);
			sectorId = (ulong)parameters[1];

			return result;
		}

		public MyObjectBuilder_Checkpoint GetServerCheckpoint(string worldName, out ulong worldId)
		{
			object[] parameters = new object[] { worldName, null };
			MyObjectBuilder_Checkpoint result = (MyObjectBuilder_Checkpoint)m_getServerCheckpoint.Invoke(null, parameters);
			worldId = (ulong)parameters[1];

			return result;
		}

		public static B337879D0C82A5F9C44D51D954769590.B3531963E948FB4FA1D057C4340C61B4 GetMainGameInstance()
		{
			FieldInfo mainGameInstanceField = m_mainGameType.GetField("392503BDB6F8C1E34A232489E2A0C6D4", BindingFlags.Static | BindingFlags.Public);
			B337879D0C82A5F9C44D51D954769590.B3531963E948FB4FA1D057C4340C61B4 mainGame = (B337879D0C82A5F9C44D51D954769590.B3531963E948FB4FA1D057C4340C61B4)mainGameInstanceField.GetValue(null);

			SetupMainGameEventHandlers(mainGame);

			return mainGame;
		}

		public static void SetNullRender(bool nullRender)
		{
			m_serverCoreNullRender.SetValue(null, nullRender);
		}

		public static void SetConfigWorld(string worldName)
		{
			MyConfigDedicatedData config = GetServerConfig();

			m_setConfigWorldName.Invoke(GetServerConfigContainer(), new object[] { worldName });
		}

		public static MyLog GetMyLog()
		{
			FieldInfo myLogField = m_mainGameType.GetField("1976E5D4FE6E8C1BD369273DEE0025AC", BindingFlags.Public | BindingFlags.Static);
			MyLog log = (MyLog)myLogField.GetValue(null);

			return log;
		}

		public static void TestSteamAPI()
		{
			try
			{
				SteamServerAPI serverAPI = SteamServerAPI.Instance;
				GameServer gameServer = serverAPI.GameServer;
				ulong serverSteamId = gameServer.GetSteamID();

				Console.WriteLine("DEBUG - Steam ID: " + serverSteamId.ToString());
			}
			catch (Exception ex)
			{
				//TODO - Do something with this exception
				return;
			}
		}

		public static void KickPlayer(ulong steamId)
		{
			SteamServerAPI serverAPI = SteamServerAPI.Instance;
			serverAPI.GameServer.SendUserDisconnect(steamId);
		}

		public static void EnableFactions(bool enabled = true)
		{
			//Force initialization just in case because this method can be called from the UI
			GetInstance();

			Type gameConstantsType = m_assembly.GetType("00DD5482C0A3DF0D94B151167E77A6D9.5FBC15A83966C3D53201318E6F912741");
			FieldInfo factionsEnabledField = gameConstantsType.GetField("AE3FD6A65A631D2BF9835EE8E86F8110", BindingFlags.Public | BindingFlags.Static);
			bool currentValue = (bool)factionsEnabledField.GetValue(null);

			Console.WriteLine("Changing 'Factions' enabled from '" + currentValue.ToString() + "' to '" + enabled.ToString() + "'");

			factionsEnabledField.SetValue(null, enabled);
		}

		#endregion
	}
}
