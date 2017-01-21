using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using FluentAssertions;
using Microsoft.Azure.WebJobs.Extensions;
using Xunit;

namespace Function1.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="Function"/> class.
    /// </summary>
    public class FunctionTest
    {
        [Theory]
        [InlineData("Success Story")]
        [InlineData("Dependency")]
        public async void HttpTriggerShouldSuccessWhenQueryParameterExists(string name)
        {
            // Arrange
            var req = new HttpRequestMessage
            {
                Content = new StringContent(string.Empty),
                RequestUri = new Uri($"http://localhost?name={name}"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
            };

            var log = new TraceMonitor();

            // Act
            var result = await Function.Run(req, log).ConfigureAwait(false);

            // Assert
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            content.Should().ContainEquivalentOf($"Hello {name}");
        }

        [Fact]
        public async void HttpTriggerShouldFailForMissingQueryParamter()
        {
            // Arrange
            var req = new HttpRequestMessage
            {
                Content = new StringContent(string.Empty),
                RequestUri = new Uri($"http://localhost"),
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } },
            };

            var log = new TraceMonitor();

            // Act
            var result = await Function.Run(req, log).ConfigureAwait(false);

            // Assert
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            content.Should().ContainEquivalentOf("Please pass a name on the query string or in the request body", content);
        }
    }
}
