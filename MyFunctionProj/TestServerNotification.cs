using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyFunctionProj
{
    public static class TestServerNotification
    {
        [FunctionName("TestServerNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var response = "The response text";

            return new OkObjectResult($"GetDisplayUrl, {response}");
        }
    }


    //public static class TestServerNotification
    //{
    //    [FunctionName("TestServerNotification")]
    //    public static async Task<IActionResult> Run(
    //        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
    //        ILogger log)
    //    {
    //        log.LogInformation("C# HTTP trigger function processed a request.");

    //        var response = "The response text";

    //        return new OkObjectResult($"GetDisplayUrl, {response}");
    //    }
    //}

}