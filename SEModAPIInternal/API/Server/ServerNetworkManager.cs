﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using SEModAPIInternal.API.Common;
using SEModAPIInternal.API.Entity;
using SEModAPIInternal.Support;
using SteamSDK;

namespace SEModAPIInternal.API.Server
{
	public class ServerNetworkManager : NetworkManager
	{
		#region "Attributes"

		new protected static ServerNetworkManager m_instance;

		public static string ServerNetworkManagerNamespace = "C42525D7DE28CE4CFB44651F3D03A50D";
		public static string ServerNetworkManagerClass = "3B0B7A338600A7B9313DE1C3723DAD14";

        //public static string ServerNetworkManagerDisconnectPlayerMethod = "3EA4ED71531B0189F424CC7CD66E6524";
        //public static string ServerNetworkManagerSetPlayerBannedMethod = "3BC84E337491E12D9F0F897FB9134924";
        //public static string ServerNetworkManagerKickPlayerMethod = "7A667D4760720DD68EC6030781B19BF5";
		public static string ServerNetworkManagerDisconnectPlayerMethod = "3EA4ED71531B0189F424CC7CD66E6524";
		public static string ServerNetworkManagerSetPlayerBannedMethod = "BEE88A8EAB5541D54CFA76A516176E33";
		public static string ServerNetworkManagerKickPlayerMethod = "AA8A18C19F361D1597F92DAAA6BBCAC7"; 
 
        public static string ServerNetworkManagerConnectedPlayersField = "89E92B070228A8BC746EFB57A3F6D2E5";

		#endregion

		#region "Properties"

		new public static ServerNetworkManager Instance
		{
			get
			{
				if (m_instance == null)
					m_instance = new ServerNetworkManager();

				return m_instance;
			}
		}

		#endregion

		#region "Methods"

		new public static bool ReflectionUnitTest()
		{
			try
			{
				Type type1 = SandboxGameAssemblyWrapper.Instance.GetAssemblyType(ServerNetworkManagerNamespace, ServerNetworkManagerClass);
				if (type1 == null)
					throw new Exception("Could not find internal type for ServerNetworkManager");
				bool result = true;
				result &= BaseObject.HasMethod(type1, ServerNetworkManagerSetPlayerBannedMethod);
				result &= BaseObject.HasMethod(type1, ServerNetworkManagerDisconnectPlayerMethod);
				result &= BaseObject.HasMethod(type1, ServerNetworkManagerKickPlayerMethod);
				result &= BaseObject.HasField(type1, ServerNetworkManagerConnectedPlayersField);

				return result;
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLine(ex);
				return false;
			}
		}

		public override List<ulong> GetConnectedPlayers()
		{
			try
			{
				Object steamServerManager = GetNetworkManager();
				if (steamServerManager == null)
					return new List<ulong>();

				FieldInfo connectedPlayersField = steamServerManager.GetType().GetField(ServerNetworkManagerConnectedPlayersField, BindingFlags.NonPublic | BindingFlags.Instance);
				List<ulong> connectedPlayers = (List<ulong>)connectedPlayersField.GetValue(steamServerManager);

				return connectedPlayers;
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLine(ex);
				return new List<ulong>();
			}
		}

		public void SetPlayerBan(ulong remoteUserId, bool isBanned)
		{
			try
			{
				Object steamServerManager = GetNetworkManager();
				BaseObject.InvokeEntityMethod(steamServerManager, ServerNetworkManagerSetPlayerBannedMethod, new object[] { remoteUserId, isBanned });

				KickPlayer(remoteUserId);
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLine(ex);
			}
		}

		public void KickPlayer(ulong remoteUserId)
		{
			try
			{
				Object steamServerManager = GetNetworkManager();
				BaseObject.InvokeEntityMethod(steamServerManager, ServerNetworkManagerKickPlayerMethod, new object[] { remoteUserId });
				BaseObject.InvokeEntityMethod(steamServerManager, ServerNetworkManagerDisconnectPlayerMethod, new object[] { remoteUserId, ChatMemberStateChangeEnum.Kicked });
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLine(ex);
			}
		}

		#endregion
	}
}
