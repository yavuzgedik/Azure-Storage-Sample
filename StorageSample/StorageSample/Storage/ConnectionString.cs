using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorageSample.Storage
{
    public class ConnectionString
    {
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = 
                string.Format
                ("DefaultEndpointsProtocol=https;" +
                "AccountName=_accountName;" +
                "AccountKey=_accountKey;" +
                "EndpointSuffix=core.windows.net");

            return CloudStorageAccount.Parse(connectionString);
        }
    }
}