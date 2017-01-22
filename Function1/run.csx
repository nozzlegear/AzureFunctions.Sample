#r "Function1.dll"
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using Function1.Function1;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    Function1.Run(req, log);
}
