using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MyFunctionProj
{
    public static class EnvironmentVariables
    {
        [FunctionName("EnvironmentVariables")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var environmentVariables = string.Join(", ", 
                Environment.GetEnvironmentVariables().Keys.Cast<object>().Select(o => o.ToString()));

            return new OkObjectResult($"EnvironmentVariables: {environmentVariables}");
        }
    }
}