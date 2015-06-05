﻿namespace SEModAPIInternal.API.Common
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
	using System.Threading;
	using Sandbox;
	using Sandbox.Common.ObjectBuilders;
	using SEModAPI.API;
	using SEModAPIInternal.API.Entity;
	using SEModAPIInternal.Support;
	using VRage.FileSystem;
	using VRage.ObjectBuilders;

	public class SandboxGameAssemblyWrapper
	{
		#region "Attributes"

		private Assembly m_assembly;

		protected static SandboxGameAssemblyWrapper m_instance;
		protected static Thread m_gameThread;

		protected bool m_isGameLoaded;

		protected DateTime m_lastProfilingOutput;
		protected int m_countQueuedActions;
		protected double m_averageQueuedActions;

		/////////////////////////////////////////////////////////////////////////////

		public static string MainGameNamespace = "Sandbox";
		public static string MainGameClass = "MySandboxGame";

		public static string MainGameEnqueueActionMethod = "Invoke";
		public static string MainGameGetTimeMillisMethod = "get_TotalGamePlayTimeInMilliseconds";
		public static string MainGameExitMethod = "EndLoop";
		public static string MainGameDisposeMethod = "Dispose";

		public static string MainGameInstanceField = "Static";
		public static string MainGameConfigContainerField = "ConfigDedicated";
		public static string IsFirstUpdateDoneField = "isFirstUpdateDone";
		public static string MainGameOnGameLoadedEvent = "OnGameLoaded";
		public static string MainGameMyLogField = "Log";

		/////////////////////////////////////////////////////////////////////////////

		public static string ServerCoreNamespace = "Sandbox.Engine.Platform";
		public static string ServerCoreClass = "Game";

		public static string ServerCoreNullRenderField = "IsDedicated";

		/////////////////////////////////////////////////////////////////////////////

		public static string GameConstantsNamespace = "Sandbox.Engine.Utils";
		public static string GameConstantsClass = "MyFakes";

		/////////////////////////////////////////////////////////////////////////////

		public static string CubeBlockObjectFactoryNamespace = "Sandbox.Game.Entities.Cube";
		public static string CubeBlockObjectFactoryClass = "MyCubeBlockFactory";

		public static string CubeBlockObjectFactoryGetBuilderFromEntityMethod = "CreateObjectBuilder";

		/////////////////////////////////////////////////////////////////////////////

		public static string EntityBaseObjectFactoryNamespace = "Sandbox.Game.Entities";
		public static string EntityBaseObjectFactoryClass = "MyEntityFactory";
		public static string EntityBaseObjectFactoryGetBuilderFromEntityMethod = "CreateObjectBuilder";
		
		////////////////////////////////////////////////////////////////////////////////
		private const string MyAPIGatewayNamespace = "Sandbox.ModAPI";
		
		private const string MyAPIGatewayClass = "MyAPIGateway";

		#endregion "Attributes"

		#region "Constructors and Initializers"

		protected SandboxGameAssemblyWrapper( )
		{
			m_instance = this;
			ExtenderOptions.UseCommonProgramData = false;
			IsInSafeMode = false;
			m_gameThread = null;

			string assemblyPath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "Sandbox.Game.dll" );
			m_assembly = Assembly.UnsafeLoadFrom( assemblyPath );

			m_lastProfilingOutput = DateTime.Now;
			m_countQueuedActions = 0;
			m_averageQueuedActions = 0;

			ApplicationLog.BaseLog.Info( "Finished loading SandboxGameAssemblyWrapper" );
		}

		#endregion "Constructors and Initializers"sss

		#region "Properties"

		public static SandboxGameAssemblyWrapper Instance
		{
			get
			{
				if ( m_instance == null )
				{
					m_instance = new SandboxGameAssemblyWrapper( );
				}

				return m_instance;
			}
		}

		public static bool IsInSafeMode { get; set; }

		public static Type MainGameType
		{
			get
			{
				Type type = Instance.GetAssemblyType( MainGameNamespace, MainGameClass );
				return type;
			}
		}

		public static Type ServerCoreType
		{
			get
			{
				Type type = Instance.GetAssemblyType( ServerCoreNamespace, ServerCoreClass );
				return type;
			}
		}

		public static Type GameConstantsType
		{
			get
			{
				Type type = Instance.GetAssemblyType( GameConstantsNamespace, GameConstantsClass );
				return type;
			}
		}

		public static Type CubeBlockObjectFactoryType
		{
			get
			{
				Type type = Instance.GetAssemblyType( CubeBlockObjectFactoryNamespace, CubeBlockObjectFactoryClass );
				return type;
			}
		}

		public static Type EntityBaseObjectFactoryType
		{
			get
			{
				Type type = Instance.GetAssemblyType( EntityBaseObjectFactoryNamespace, EntityBaseObjectFactoryClass );
				return type;
			}
		}

		public static MySandboxGame MainGame
		{
			get
			{
				try
				{
					return MySandboxGame.Static;
				}
				catch ( Exception ex )
				{
					ApplicationLog.BaseLog.Error( ex );
					return null;
				}
			}
		}

		public static Type APIGatewayType
		{
			get
			{
				Type type = Instance.GetAssemblyType( MyAPIGatewayNamespace, MyAPIGatewayClass );
				return type;
			}
		}

		#endregion "Properties"

		#region "Methods"

		private void EntitiesLoadedEvent( )
		{
			try
			{
				ApplicationLog.BaseLog.Info( "MainGameEvent - Entity loading complete" );

				//TODO - Do stuff
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
			}
		}

		public static bool ReflectionUnitTest( )
		{
			try
			{
				Type type = MainGameType;
				if ( type == null )
					throw new Exception( "Could not find internal type for MainGame" );
				bool result = true;
				if ( !BaseObject.HasMethod( type, MainGameEnqueueActionMethod ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", MainGameEnqueueActionMethod );
				}
				if ( !BaseObject.HasMethod( type, MainGameGetTimeMillisMethod ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", MainGameGetTimeMillisMethod );
				}
				if ( !BaseObject.HasMethod( type, MainGameExitMethod ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", MainGameExitMethod );
				}
				if ( !BaseObject.HasMethod( type, MainGameDisposeMethod ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", MainGameDisposeMethod );
				}
				if ( !BaseObject.HasField( type, MainGameInstanceField ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", MainGameInstanceField );
				}
				if ( !BaseObject.HasField( type, MainGameConfigContainerField ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", MainGameConfigContainerField );
				}
				if ( !BaseObject.HasField( type, IsFirstUpdateDoneField ) )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find method {0}", IsFirstUpdateDoneField );
				}
				if ( type.GetEvent( "OnGameLoaded" ) == null )
				{
					result = false;
					ApplicationLog.BaseLog.Error( "Could not find event {0}", MainGameOnGameLoadedEvent );
				}

				Type type2 = ServerCoreType;
				if ( type2 == null )
					throw new Exception( "Could not find physics manager type for ServerCore" );
				result &= BaseObject.HasField( type2, ServerCoreNullRenderField );

				Type type3 = GameConstantsType;
				if ( type3 == null )
					throw new Exception( "Could not find physics manager type for GameConstants" );

				return result;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return false;
			}
		}

		public MyObjectBuilder_CubeBlock GetCubeBlockObjectBuilderFromEntity( Object entity )
		{
			try
			{
				MyObjectBuilder_CubeBlock newObjectBuilder = (MyObjectBuilder_CubeBlock)BaseObject.InvokeStaticMethod( CubeBlockObjectFactoryType, CubeBlockObjectFactoryGetBuilderFromEntityMethod, new[ ] { entity } );

				return newObjectBuilder;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return null;
			}
		}

		public MyObjectBuilder_EntityBase GetEntityBaseObjectBuilderFromEntity( Object entity )
		{
			try
			{
				MyObjectBuilder_EntityBase newObjectBuilder = (MyObjectBuilder_EntityBase)BaseObject.InvokeStaticMethod( EntityBaseObjectFactoryType, EntityBaseObjectFactoryGetBuilderFromEntityMethod, new[ ] { entity } );

				return newObjectBuilder;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return null;
			}
		}

		public bool EnqueueMainGameAction( Action action )
		{
			try
			{
				if ( Thread.CurrentThread == m_gameThread )
				{
					action( );
					return true;
				}

				BaseObject.InvokeEntityMethod( MainGame, MainGameEnqueueActionMethod, new object[ ] { action } );

				if ( ExtenderOptions.IsDebugging )
				{
					UpdateProfile( );
				}

				return true;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return false;
			}
		}

		public bool EnqueueMainGameAction<T>( Action<T> action, T arg )
		{
			try
			{
				if ( Thread.CurrentThread == m_gameThread )
				{
					action( arg );
					return true;
				}

				BaseObject.InvokeEntityMethod( MainGame, MainGameEnqueueActionMethod, new object[ ] { action, arg } );

				if ( ExtenderOptions.IsDebugging )
				{
					UpdateProfile( );
				}

				return true;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return false;
			}
		}

		private void UpdateProfile( )
		{
			m_countQueuedActions++;

			TimeSpan timeSinceLastProfilingOutput = DateTime.Now - m_lastProfilingOutput;
			if ( timeSinceLastProfilingOutput.TotalSeconds > 60 )
			{
				m_averageQueuedActions = m_countQueuedActions / timeSinceLastProfilingOutput.TotalSeconds;

				ApplicationLog.BaseLog.Info( "Average actions queued per second: {0}", Math.Round( m_averageQueuedActions, 2 ) );

				m_countQueuedActions = 0;
				m_lastProfilingOutput = DateTime.Now;
			}
		}

		public delegate void GameActionCallback( Object state );

		public bool BeginGameAction( Action action, GameActionCallback callback, Object state )
		{
			try
			{
				ThreadPool.QueueUserWorkItem( o =>
				{
					AutoResetEvent e = new AutoResetEvent( false );

					/*
					MyAPIGateway.Utilities.InvokeOnGameThread(() =>
					{
						if (m_gameThread == null)
						{
							m_gameThread = Thread.CurrentThread;
						}

						action();
						e.Set();
					});
					 */ 
					Instance.EnqueueMainGameAction(() =>
					{
						if ( m_gameThread == null )
						{
							m_gameThread = Thread.CurrentThread;
						}

						action( );
						e.Set( );
					} );
					
					e.WaitOne( );

					if ( callback != null )
					{
						callback( state );
					}
				} );

				return true;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return false;
			}
		}

		public bool GameAction( Action action )
		{
			try
			{
				AutoResetEvent e = new AutoResetEvent( false );
				/*
				MyAPIGateway.Utilities.InvokeOnGameThread(() =>
				{
					if (m_gameThread == null)
					{
						m_gameThread = Thread.CurrentThread;
					}

					action();
					e.Set();
				});
				*/

				Instance.EnqueueMainGameAction( ( ) =>
				{
					if ( m_gameThread == null )
					{
						m_gameThread = Thread.CurrentThread;
					}

					action( );
					e.Set( );
				} );

				e.WaitOne( );
				return true;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return false;
			}
		}

		public void SetNullRender( bool nullRender )
		{
			try
			{
				BaseObject.SetStaticFieldValue( ServerCoreType, ServerCoreNullRenderField, nullRender );
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
			}
		}

		public Type GetAssemblyType( string namespaceName, string className )
		{
			try
			{
				Type type = m_assembly.GetType( namespaceName + "." + className );

				return type;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return null;
			}
		}

		public int GetMainGameMilliseconds( )
		{
			try
			{
				int gameTimeMillis = (int)BaseObject.InvokeStaticMethod( MainGameType, MainGameGetTimeMillisMethod );

				return gameTimeMillis;
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error( ex );
				return 0;
			}
		}

		public List<string> GetCommonInstanceList( )
		{
			List<string> result = new List<string>( );

			try
			{
				string commonPath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ), "SpaceEngineersDedicated" );
				if ( Directory.Exists( commonPath ) )
				{
					string[ ] subDirectories = Directory.GetDirectories( commonPath );
					foreach ( string fullInstancePath in subDirectories )
					{
						string[ ] directories = fullInstancePath.Split( new[ ] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar } );
						string instanceName = directories[ directories.Length - 1 ];

						result.Add( instanceName );
					}
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.BaseLog.Error(  ex.ToString( ) );
			}

			return result;
		}

		#endregion "Methods"

	}
}