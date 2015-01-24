﻿using System;
using System.Runtime.Serialization;
using Sandbox.Common.ObjectBuilders;

namespace SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock
{
	[DataContract(Name = "ShipWelderEntityProxy")]
	public class ShipWelderEntity : ShipToolBaseEntity
	{
		#region "Attributes"

		public static string ShipWelderNamespace = "";
		public static string ShipWelderClass = "";

		#endregion

		#region "Constructors and Intializers"

		public ShipWelderEntity(CubeGridEntity parent, MyObjectBuilder_ShipWelder definition)
			: base(parent, definition)
		{
		}

		public ShipWelderEntity(CubeGridEntity parent, MyObjectBuilder_ShipWelder definition, Object backingObject)
			: base(parent, definition, backingObject)
		{
		}

		#endregion

		#region "Properties"
		#endregion

		#region "Methods"
		#endregion
	}
}
