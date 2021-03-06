﻿using System;
using System.Configuration;
using CHAOS.Portal.Client;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Extensions;
using CHAOS.Portal.Client.Standard;

namespace GetObjectInfo
{
	class Program
	{
		private IPortalClient _client;
		private Configuration _configuration;

		static void Main(string[] args)
		{
			try
			{
				new Program().Run();
			}
			catch (Exception exception)
			{
				Console.WriteLine("An error occured: {0}", exception.Message);
			}
			
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		public Program()
		{
			_configuration = new Configuration();
		}

		private void Run()
		{
			Console.WriteLine("Initializing...");
			Initialize();
			GetObjects();
		}


		private void Initialize()
		{
			_client = new PortalClient
			{
				ServicePath = _configuration.ServicePath
			};

			var authKeyResponse = _client
				.AuthKey() //The extension to call
				.Login(_configuration.AccessToken) //Authentication methods automatically updates the client instance so it knows it's authenticated, it also creates a session
				.Synchronous() //Halts the current thread until the call is completed
				.ThrowError() //Throws an exception if the service call returned an error, this must be after the call is completed ("Synchronous()" insures this)
				.Response; //Returns the response from the service, this must be after the call is completed ("Synchronous()" insures this)

			Console.WriteLine("Session authenticated, user guid is: {0}", authKeyResponse.Body.Results[0].UserGuid);
		}

		private void GetObjects()
		{
			var response =_client.Object()
					.Get(null, _configuration.FolderId, null, false, true, true, false, false, 10, 0)
					.Synchronous()
					.Response;

			if (response.Error != null) //Handling the error manually
				throw new Exception("Error getting objects: " + response.Error.Message);

			Console.WriteLine("Got {0} objects out of {1}", response.Body.Count, response.Body.TotalCount);

			foreach (var @object in response.Body.Results)
			{
				Console.WriteLine("Object: {0} Files: {1} Metadata: {2}", @object.Id, @object.Files == null ? 0 : @object.Files.Count, @object.Metadatas == null ? 0 : @object.Metadatas.Count);
			}
		}
	}
}
