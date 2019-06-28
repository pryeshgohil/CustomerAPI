using CustomerAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerAPITest
{
    public class CustomerAPIIntegrationTest
     : IClassFixture<WebApplicationFactory<CustomerAPI.Startup>>
    {
        private readonly WebApplicationFactory<CustomerAPI.Startup> _factory;

        public CustomerAPIIntegrationTest(WebApplicationFactory<CustomerAPI.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_Get_All()
        {
            string url = "/api/customers";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Test_Get_Single(int id)
        {
            string url = $"/api/customers/{id}";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Test_Post_Single()
        {
            string url = $"/api/customers";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync(url,
                new StringContent(
                    JsonConvert.SerializeObject(new Customer()
                    {
                        FirstName = "Example",
                        Surname = "Example",
                        EmailAddress = "example@gmail.com",
                        Password = "testingtesting123"
                    }), Encoding.UTF8, "application/json" ));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        [Theory]
        [InlineData(4)]
        public async Task Test_Delete_Single(int id)
        {
            string url = $"/api/customers/{id}";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

    }
}
