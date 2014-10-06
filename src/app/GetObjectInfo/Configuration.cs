using System;
using System.Configuration;

namespace GetObjectInfo
{
	public class Configuration
	{
		public Configuration()
		{
			ServicePath = ConfigurationManager.AppSettings["ServicePath"];
			AccessToken = ConfigurationManager.AppSettings["AccessToken"];
			ObjectTypeId = uint.Parse(ConfigurationManager.AppSettings["ObjectTypeId"]);
			FolderId = uint.Parse(ConfigurationManager.AppSettings["FolderId"]);
			MetadataSchemaGuid = Guid.Parse(ConfigurationManager.AppSettings["MetadataSchemaGuid"]); 	
		}

		public Guid MetadataSchemaGuid { get; set; }
		public uint FolderId { get; set; }
		public uint ObjectTypeId { get; set; }
		public string AccessToken { get; set; }
		public string ServicePath { get; set; }
	}
}