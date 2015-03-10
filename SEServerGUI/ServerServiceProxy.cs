﻿namespace SEServerGUI
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel;
	using SEComm;
	using SEModAPIInternal;
	using SEModAPIInternal.API.Entity.Sector.SectorObject;

	public sealed class ServerServiceProxy : ClientBase<IServerService>, IServerService
	{
		public ServerServiceProxy( )
			: base( "ServerService" )
		{

		}

		public ServerServiceProxy( string endpointConfigurationName )
			: base( endpointConfigurationName )
		{

		}

		public StartServerResponse StartServer( StartServerRequest request )
		{
			return Channel.StartServer( request );
		}

		public void StopServer( )
		{
			Channel.StopServer( );
		}

		public void Exit( int exitCode )
		{
			Channel.Exit( exitCode );
		}

		public IEnumerable<ChatUserItem> GetPlayersOnline( )
		{
			return Channel.GetPlayersOnline( );
		}

		public void KickPlayer( ulong steamId )
		{
			Channel.KickPlayer( steamId );
		}

		public void BanPlayer( ulong steamId )
		{
			Channel.BanPlayer( steamId );
		}

		public void UnBanPlayer( ulong steamId )
		{
			Channel.UnBanPlayer( steamId );
		}
	}
}