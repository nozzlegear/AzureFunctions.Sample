using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using Microsoft.Azure.WebJobs.Host;

namespace Function1
{
    public class Class
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            // parse query parameter
            var name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", StringComparison.OrdinalIgnoreCase) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, $"Hello {name}");
        }
    }
}