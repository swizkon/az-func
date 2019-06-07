#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public static IActionResult Run(HttpRequest req, ICollector<string> outputQueueItems, TraceWriter log)
{
    for (var i = 1; i < 10; i++)
    {
        var item = $"Notification {i}";

        log.Info(item);
        outputQueueItems.Add(item);
    }
    
    return new OkObjectResult($"Did some queing at " + DateTime.Now);
}
