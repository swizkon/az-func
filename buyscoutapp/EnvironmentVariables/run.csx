#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public static IActionResult Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    var environmentVariables = string.Join("\n\t",
        Environment.GetEnvironmentVariables().Keys.Cast<object>().Select(o => o.ToString()));

    return new OkObjectResult($"EnvironmentVariables:\n\t {environmentVariables}");
}
