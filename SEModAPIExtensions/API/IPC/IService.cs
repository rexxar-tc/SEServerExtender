﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using System.ServiceModel.Web;
using System.Security.Permissions;

namespace SEModAPIExtensions.API.IPC
{
	[ServiceContract]
	public interface IWebServiceContract
	{
		[OperationContract]
		[WebInvoke(Method = "OPTIONS", UriTemplate = "")]
		void GetOptions();

		[OperationContract]
		[WebGet]
		List<BaseEntity> GetSectorEntities();

		[OperationContract]
		[WebGet]
		List<CubeGridEntity> GetSectorCubeGridEntities();

		[OperationContract]
		[WebGet(UriTemplate = "GetCubeBlocks/{cubeGridEntityId}")]
		List<CubeBlockEntity> GetCubeBlocks(string cubeGridEntityId);
	}
}
