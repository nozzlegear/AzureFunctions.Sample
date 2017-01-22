#r "bin\Release\Function.dll"
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;
using Function;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    Function.run(req, log);
}
