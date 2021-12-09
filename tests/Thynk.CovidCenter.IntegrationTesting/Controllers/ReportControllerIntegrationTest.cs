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
    public class ReportControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CovidCenterDbContext covidCenterDb { get; set; }
        public ReportControllerIntegrationTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            covidCenterDb = factory.covidCenterDb;
        }

        [Fact]
        public async Task CreateBookingResult_ValidModel_ReturnsSuccessMessages()
        {
            var labAdmin = covidCenterDb.Set<ApplicationUser>().Where(x => x.UserRole == UserRole.LabAdministrator).FirstOrDefault();
            var booking = covidCenterDb.Set<Booking>().ToList();

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Report/booking-result"),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "ID", booking[1].ID.ToString() },
                { "BookingResult", "Positive" },
                { "ApplicationUserId", labAdmin.ID.ToString() },
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
        public async Task GetResult_ValidModel_ReturnsSuccessMessages()
        {
            var labAdmin = covidCenterDb.Set<ApplicationUser>().Where(x => x.UserRole == UserRole.LabAdministrator).FirstOrDefault();
            var booking = covidCenterDb.Set<Booking>().FirstOrDefault();

            var resultRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Report/booking-result"),
                Method = HttpMethod.Post
            };

            resultRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var resultRequestModel = new Dictionary<string, string>
            {
                { "ID", booking.ID.ToString() },
                { "BookingResult", "Positive" },
                { "ApplicationUserId", labAdmin.ID.ToString() },
            };

            string resultJson = JsonConvert.SerializeObject(resultRequestModel);

            resultRequest.Content = new StringContent(resultJson, System.Text.Encoding.UTF8, "application/json");

            await _client.SendAsync(resultRequest);

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Report/get-results"),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "LocationId", booking.LocationID.ToString() },
                { "ApplicationUserId", labAdmin.ID.ToString() },
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
    }
}
