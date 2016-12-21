﻿using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WinEchek.Core.Network
{
    public class NetworkGameServiceHost
    {
        private ServiceHost _host;
        public NetworkGameService NetworkGameService { get; set; }

        public NetworkGameServiceHost(Uri uri)
        {
            NetworkGameService = new NetworkGameService();

            _host = new ServiceHost(NetworkGameService, uri);
            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            _host.Description.Behaviors.Add(smb);
        }

        public void Open()
        {
            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            _host.Open();
        }

        public void Close()
        {
            _host.Close();
        }
    }
}