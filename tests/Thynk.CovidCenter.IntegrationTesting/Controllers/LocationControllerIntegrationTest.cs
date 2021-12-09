using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Thynk.CovidCenter.API;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Enums;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository;
using Xunit;

namespace Thynk.CovidCenter.IntegrationTesting.Controllers
{
    public class LocationControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CovidCenterDbContext covidCenterDb { get; set; }
        public LocationControllerIntegrationTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            covidCenterDb = factory.covidCenterDb;
        }

        [Fact]
        public async Task CreateLocation_ValidModel_ReturnsSuccessMessages()
        {
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Location/create-location"),
                Method = HttpMethod.Post
            };
            
            var adminUser = covidCenterDb.Set<ApplicationUser>().ToList();

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "Name", "Test" },
                { "Description", "Test" },
                { "ApplicationUserId", adminUser.FirstOrDefault(x => x.UserRole == UserRole.Administrator).ID.ToString() }
            };

            string json = JsonConvert.SerializeObject(requestModel);

            postRequest.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();


            var responseObject = JsonConvert.DeserializeObject<BaseResponse>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(responseObject.Status);
        }

        [Fact]
        public async Task GetLocations_ReturnsSuccessMessages()
        {
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Location/get-locations"),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();


            var responseObject = JsonConvert.DeserializeObject<BaseResponse>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(responseObject.Status);
        }

        [Fact]
        public async Task GetLocation_ValidModel_ReturnsSuccessMessages()
        {
            var location = covidCenterDb.Set<Location>().ToList();
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Location/get-location?locationId="+ location.FirstOrDefault().ID),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();


            var responseObject = JsonConvert.DeserializeObject<BaseResponse>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(responseObject.Status);
        }
    }
}
