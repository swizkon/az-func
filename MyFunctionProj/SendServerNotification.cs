using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyFunctionProj
{
    public static class SendServerNotification
    {
        [FunctionName("SendServerNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [SignalR(HubName = "chat", ConnectionStringSetting = "SignalRService")]IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            
            await signalRMessages.AddAsync(new SignalRMessage
            {
                Target = "echo",
                Arguments = new object[] {"Az Func", name }
            });
            
            return new OkObjectResult($"Hello, {name}");
        }

        private static string GetEndPointAddress()
        {
            return Environment.GetEnvironmentVariable("TestServerNotificationEndPoint")
                   ?? "http://localhost:7069/api/TestServerNotification";
        }
    }
}
