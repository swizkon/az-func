using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyFunctionProj
{
    public static class SendServerNotification
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("SendServerNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var body = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("plan", "123"), 
                new KeyValuePair<string, string>("task", "12345"), 
                new KeyValuePair<string, string>("task", "12345")
            });

            var response = await httpClient.PostAsync(GetEndPointAddress(), body);
            var result = await response.Content.ReadAsStringAsync();
            return new OkObjectResult($"Hello, {response.StatusCode} {result}");
        }

        private static string GetEndPointAddress()
        {
            return Environment.GetEnvironmentVariable("ServerNotificationAddress")
                   ?? "http://localhost:7069/api/TestServerNotification";
        }
    }
}
