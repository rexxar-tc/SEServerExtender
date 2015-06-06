﻿namespace SEModAPI.API.Utility
{
	using System;
	using System.Reflection;
	using NLog;

	public static class Reflection
	{
		private static readonly Logger Log = LogManager.GetLogger( "BaseLog" );
		public static dynamic GetEntityFieldValue( Object gameEntity, string fieldName, bool suppressErrors = false )
		{
			try
			{
				FieldInfo field = GetEntityField( gameEntity, fieldName, suppressErrors );
				if ( field == null )
					return null;
				dynamic value = field.GetValue( gameEntity );
				return value;
			}
			catch ( Exception ex )
			{
				if ( !suppressErrors )
					Log.Error( ex );
				return null;
			}
		}

		public static FieldInfo GetEntityField( Object gameEntity, string fieldName, bool suppressErrors = false )
		{
			try
			{
				FieldInfo field = gameEntity.GetType( ).GetField( fieldName );
				if ( field == null )
				{
					//Recurse up through the class heirarchy to try to find the field
					Type type = gameEntity.GetType( );
					while ( type != typeof( Object ) )
					{
						field = type.GetField( fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy );
						if ( field != null )
							break;

						type = type.BaseType;
					}
				}
				return field;
			}
			catch ( Exception ex )
			{
				if ( !suppressErrors )
				{
					Log.Error( "Failed to get entity field '{0}'", fieldName );
					Log.Error( ex );
				}
				return null;
			}
		}

		public static bool HasMethod( Type objectType, string methodName )
		{
			return Reflection.HasMethod( objectType, methodName, null );
		}


		public static bool HasMethod( Type objectType, string methodName, Type[ ] argTypes )
		{
			try
			{
				if ( String.IsNullOrEmpty( methodName ) )
					return false;

				if ( argTypes == null )
				{
					MethodInfo method = objectType.GetMethod( methodName );
					if ( method == null )
						method = objectType.GetMethod( methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy );
					if ( method == null && objectType.BaseType != null )
						method = objectType.BaseType.GetMethod( methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy );
					if ( method == null )
					{
						if ( ExtenderOptions.IsDebugging )
							Log.Error( "Failed to find method '" + methodName + "' in type '" + objectType.FullName + "'" );
						return false;
					}
				}
				else
				{
					MethodInfo method = objectType.GetMethod( methodName, argTypes );
					if ( method == null )
						method = objectType.GetMethod( methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy, Type.DefaultBinder, argTypes, null );
					if ( method == null && objectType.BaseType != null )
						method = objectType.BaseType.GetMethod( methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy, Type.DefaultBinder, argTypes, null );
					if ( method == null )
					{
						if ( ExtenderOptions.IsDebugging )
							Log.Error( "Failed to find method '" + methodName + "' in type '" + objectType.FullName + "'" );
						return false;
					}
				}

				return true;
			}
			catch ( AmbiguousMatchException aex )
			{
				return true;
			}
			catch ( Exception ex )
			{
				if ( ExtenderOptions.IsDebugging )
					Log.Error( "Failed to find method '" + methodName + "' in type '" + objectType.FullName + "': " + ex.Message );
				Log.Error( ex );
				return false;
			}
		}
	}
}
