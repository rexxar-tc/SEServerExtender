﻿using System;
using System.IO;
using System.Xml;
using Microsoft.Xml.Serialization.GeneratedAssembly;
using Sandbox.Common.ObjectBuilders.Definitions;

namespace SEModAPI.API.Definitions
{
	public class ConfigFileSerializer
	{
		private const string m_DefaultExtension = ".sbc";
		private static FileInfo m_configFileInfo;

		public ConfigFileSerializer(FileInfo configFileInfo, bool useDefaultFileName = true)
		{
			EnsureFileInfoValidity(configFileInfo, useDefaultFileName);
			m_configFileInfo = configFileInfo;
		}

		/// <summary>
		/// This method is intended to verify of the given configFileInfo is valid
		/// </summary>
		/// <param name="configFileInfo">The valid FileInfo that points to a valid [config file name].sbc file</param>
		/// <param name="defaultName">Defines if the file has the defaultName: [config file name].sbc</param>
		private static void EnsureFileInfoValidity(FileInfo configFileInfo, bool defaultName = true)
		{
			if (configFileInfo == null)
			{
				throw new SEConfigurationException(SEConfigurationExceptionState.InvalidFileInfo, "The given configFileInfo is null.");
			}
			if (defaultName)
			{
				if (configFileInfo.Extension != m_DefaultExtension)
				{
					throw new SEConfigurationException(SEConfigurationExceptionState.InvalidDefaultConfigFileName, "The given file name is not matching the default configuration name pattern.");
				}
			}
		}

		/// <summary>
		/// Method to serialize a configuration file.
		/// </summary>
		/// <param name="definitions">The definition to serialize.</param>
		public void Serialize(MyObjectBuilder_Definitions definitions)
		{
			XmlWriterSettings settings = new XmlWriterSettings()
			{
				CloseOutput = true,
				Indent = true,
				ConformanceLevel = ConformanceLevel.Auto,
				NewLineHandling = NewLineHandling.Entitize
			};
			XmlWriter writer = XmlWriter.Create(m_configFileInfo.FullName, settings);
			MyObjectBuilder_DefinitionsSerializer serializer = (MyObjectBuilder_DefinitionsSerializer)Activator.CreateInstance(typeof(MyObjectBuilder_DefinitionsSerializer));
			serializer.Serialize(writer,definitions);
			writer.Close();
		}

		/// <summary>
		/// Method to deserialize a configuration file.
		/// </summary>
		/// <returns>The deserialized definition.</returns>
		public MyObjectBuilder_Definitions Deserialize()
		{
			if (!m_configFileInfo.Exists){
				throw new SEConfigurationException(SEConfigurationExceptionState.InvalidFileInfo, "The file pointed by configFileInfo does not exists." + "\r\n" + "Cannot deserialize: " + m_configFileInfo.FullName);
			}

			XmlReaderSettings settings = new XmlReaderSettings();
			XmlReader reader = XmlReader.Create(m_configFileInfo.FullName, settings);
			MyObjectBuilder_DefinitionsSerializer serializer = (MyObjectBuilder_DefinitionsSerializer)Activator.CreateInstance(typeof(MyObjectBuilder_DefinitionsSerializer));
			if (!serializer.CanDeserialize(reader))
			{
				throw new SEConfigurationException(SEConfigurationExceptionState.InvalidConfigurationFile, "The file pointed by configFileInfo cannot be deserialized: " + m_configFileInfo.FullName);
			}
			MyObjectBuilder_Definitions definitions = (MyObjectBuilder_Definitions) serializer.Deserialize(reader);
			reader.Close();
			return definitions;
		}
	}
}
