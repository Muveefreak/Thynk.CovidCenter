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
    public class AvailableDatesControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CovidCenterDbContext covidCenterDb { get; set; }
        public AvailableDatesControllerIntegrationTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            covidCenterDb = factory.covidCenterDb;
        }

        [Fact]
        public async Task CreateAvailableDates_ValidModel_ReturnsSuccessMessages()
        {
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/AvailableDates/create-available-dates"),
                Method = HttpMethod.Post
            };

            var individual = covidCenterDb.Set<ApplicationUser>().Where(x => x.UserRole == UserRole.Administrator).FirstOrDefault();
            var location = covidCenterDb.Set<Location>().FirstOrDefault();

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "ApplicationUserId", individual.ID.ToString() },
                { "LocationId", location.ID.ToString() },
                { "DateAvailable", Convert.ToString(DateTime.UtcNow.Date.AddDays(7)) },
                { "AvailableSlots", "10" }
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
        public async Task GetAvailableDatesByLocation_ReturnsSuccessMessages()
        {
            var location = covidCenterDb.Set<Location>().FirstOrDefault();
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/AvailableDates/get-available-dates-by-location?locationId="+ location.ID),
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
        public async Task GetAllAvailableDates_ValidModel_ReturnsSuccessMessages()
        {
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/AvailableDates/get-all-available-dates"),
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
