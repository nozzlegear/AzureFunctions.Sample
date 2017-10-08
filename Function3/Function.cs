using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Function3
{
    public class Function
    {
        public static void Run(TimerInfo myTimer, TraceWriter log)
        {
            log.Info("My C# timer is running!!!!!");
        }
    }
}