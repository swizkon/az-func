using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyFunctionProj
{
    public static class DequeueServerNotification
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("DequeueServerNotification")]
        public static async void Run([QueueTrigger("servernotifications", Connection = "QueueStorageAccount")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"DequeueServerNotification C# Queue trigger function processed: {myQueueItem}");

            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("plan", "123"),
                new KeyValuePair<string, string>("task", "12345"),
                new KeyValuePair<string, string>("message", myQueueItem)
            });

            var response = await httpClient.PostAsync(GetEndPointAddress(), body);
            var result = await response.Content.ReadAsStringAsync();

            log.LogInformation("Got StatusCode {StatusCode} and result {Result}", response.StatusCode, result);
        }

        private static string GetEndPointAddress()
        {
            return Environment.GetEnvironmentVariable("ServerNotificationAddress") ?? "http://localhost:7071/api/TestServerNotification";
        }
    }

}
/*
 *{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "QueueStorageAccount": "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"
  },
  "ConnectionStrings": {
    "QueueStorageAccount": "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"
  } 
}
 *
 */
