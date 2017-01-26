using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using FluentAssertions;
using Microsoft.Azure.WebJobs.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace Function2.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="Function"/> class.
    /// </summary>
    public class FunctionTest
    {
        [Theory]
        [InlineData("Path")]
        public async void HttpTriggerShouldSuccessWhenEnvironmentVariableExists(string envKey)
        {
            // Arrange
            var jsonContent = @"{
    ""first"": ""HttpResponse"",
    ""last"": ""Hello Azure Functions!""
}";
            var req = new HttpRequestMessage
            {
                Content = new StringContent(jsonContent),
                RequestUri = new Uri($"http://localhost"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
            };

            var log = new TraceMonitor();

            // Act
            var result = await Function.Run(req, log).ConfigureAwait(false);

            // Assert
            dynamic json = JsonConvert.DeserializeObject(jsonContent);
            var first = json.first;
            var last = json.last;
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            content.Should().ContainEquivalentOf($"Hello {first} {last}!");
        }
    }
}
