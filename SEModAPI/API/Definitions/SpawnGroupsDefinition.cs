﻿using Sandbox.Common.ObjectBuilders.Definitions;
using SEModAPI.API.Definitions;

namespace SEModAPI.API.Definitions
{
	public class SpawnGroupDefinition : OverLayerDefinition<MyObjectBuilder_SpawnGroupDefinition>
	{
		#region "Constructors and Initializers"

		public SpawnGroupDefinition(MyObjectBuilder_SpawnGroupDefinition myObjectBuilderDefinitionSubType): base(myObjectBuilderDefinitionSubType)
		{}

		#endregion

		#region "Properties"

		public float Frequency
		{
			get { return m_baseDefinition.Frequency; }
			set
			{
                if (m_baseDefinition.Frequency == value) return;
                m_baseDefinition.Frequency = value;
				Changed = true;
			}
		}

		public int PrefabCount
		{
            get { return m_baseDefinition.Prefabs.Length; }
		}

		#endregion

        #region "Methods"

        protected override string GetNameFrom(MyObjectBuilder_SpawnGroupDefinition definition)
		{
            return definition.TypeId.ToString();
		}

		#endregion
	}

	public class SpawnGroupPrefab : OverLayerDefinition<MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab>
	{
		#region "Constructors and Initializers"

		public SpawnGroupPrefab(MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab myObjectBuilderDefinitionSubType): base(myObjectBuilderDefinitionSubType)
		{}

		#endregion

		#region "Properties"

		public string File
		{
			get { return m_baseDefinition.File; }
			set
			{
                if (m_baseDefinition.File == value) return;
                m_baseDefinition.File = value;
				Changed = true;
			}
		}

		public VRageMath.Vector3 Position
		{
            get { return m_baseDefinition.Position; }
			set
			{
                if (m_baseDefinition.Position == value) return;
                m_baseDefinition.Position = value;
				Changed = true;
			}
		}

		public string BeaconText
		{
            get { return m_baseDefinition.BeaconText; }
			set
			{
                if (m_baseDefinition.BeaconText == value) return;
                m_baseDefinition.BeaconText = value;
				Changed = true;
			}
		}

		public float Speed
		{
            get { return m_baseDefinition.Speed; }
			set
			{
                if (m_baseDefinition.Speed == value) return;
                m_baseDefinition.Speed = value;
				Changed = true;
			}
		}

		#endregion

        #region "Methods"

        protected override string GetNameFrom(MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab definition)
	{
	        return definition.BeaconText;
		}

		#endregion
				}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SpawnGroupsDefinitionsManager : OverLayerDefinitionsManager<MyObjectBuilder_SpawnGroupDefinition, SpawnGroupDefinition>
				{
		#region "Constructors and Initializers"

		public SpawnGroupsDefinitionsManager(MyObjectBuilder_SpawnGroupDefinition[] definitions): base(definitions)
        {}

		#endregion

		#region "Methods"

        protected override SpawnGroupDefinition CreateOverLayerSubTypeInstance(MyObjectBuilder_SpawnGroupDefinition definition)
				{
            return new SpawnGroupDefinition(definition);
				}

        protected override MyObjectBuilder_SpawnGroupDefinition GetBaseTypeOf(SpawnGroupDefinition overLayer)
        {
            return overLayer.BaseDefinition;
			}

        protected override bool GetChangedState(SpawnGroupDefinition overLayer)
        {
            return overLayer.Changed;
		}

		public void Save()
		{
			if (!this.Changed) return;

			m_configSerializer.SpawnGroupDefinitions = this.RawDefinitions;
			m_configSerializer.SaveSpawnGroupsContentFile();
		}

		#endregion
	}

    public class SpawnGroupPrefabsManager : OverLayerDefinitionsManager<MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab, SpawnGroupPrefab>
	{
		#region "Constructors and Initializers"

		public SpawnGroupPrefabsManager(MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab[] definitions): base(definitions)
		{}

		#endregion

		#region "Methods"

        protected override SpawnGroupPrefab CreateOverLayerSubTypeInstance(MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab definition)
				{
            return new SpawnGroupPrefab(definition);
				}

        protected override MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab GetBaseTypeOf(SpawnGroupPrefab overLayer)
			{
            return overLayer.BaseDefinition;
		}

		#endregion

		#region "Methods"

		public int IndexOf(SpawnGroupPrefab spawnGroup)
		{
			int index = 0;
			bool foundMatch = false;
			foreach (var def in m_definitions)
			{
				if (def.Value == spawnGroup)
				{
            return overLayer.Changed;
		}

		#endregion
	}
}
