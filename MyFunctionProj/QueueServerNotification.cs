using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;

namespace MyFunctionProj
{
    public static class QueueServerNotification
    {
        [FunctionName("QueueServerNotification")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("servernotifications", Connection = "QueueStorageAccount")] ICollector<string> servernotifications,
            ILogger log)
        {
            for (var i = 1; i < 10; i++)
            {
                servernotifications.Add($"Server Notification {i} at {DateTime.Now}");
            }
            
            // queueCollector.Add(new Customer { FirstName = "John" });

            //ICollector<CustomQueueMessage> myQueueItems

            //var body = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("plan", "123"),
            //    new KeyValuePair<string, string>("task", "12345"),
            //    new KeyValuePair<string, string>("message", myQueueItem)
            //});
            
            log.LogInformation("Got StatusCode {QueryString}", req.QueryString);

            return new OkObjectResult(req.QueryString);
        }

        private static string GetEndPointAddress()
        {
            return Environment.GetEnvironmentVariable("ServerNotificationAddress") ?? "http://localhost:7071/api/TestServerNotification";
        }
    }
}