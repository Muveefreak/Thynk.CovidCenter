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
    public class BookingControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CovidCenterDbContext covidCenterDb { get; set; }
        public BookingControllerIntegrationTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            covidCenterDb = factory.covidCenterDb;
        }

        [Fact]
        public async Task CreateBooking_ValidModel_ReturnsSuccessMessages()
        {
            var individual = covidCenterDb.Set<ApplicationUser>().Where(x => x.UserRole == UserRole.Individual).FirstOrDefault();
            var location = covidCenterDb.Set<Location>().Where(x => x.AvailableDates.Any(c => c.AvailableSlots > 0 && c.DateAvailable >= DateTime.UtcNow.Date)).Last();

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Booking/create-booking"),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "LocationID", location.ID.ToString() },
                { "ApplicationUserId", individual.ID.ToString() },
                { "AvailableDateId", location.AvailableDates.First().ID.ToString() },
                { "TestType", "PCR" }
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
        public async Task CancelBooking_ValidModel_ReturnsSuccessMessages()
        {
            var individual = covidCenterDb.Set<ApplicationUser>().Where(x => x.UserRole == UserRole.Individual).FirstOrDefault();
            var location = covidCenterDb.Set<Location>().Where(x => x.AvailableDates.Any(c => c.AvailableSlots > 0 && c.DateAvailable > DateTime.UtcNow.Date)).First();

            var bookingRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Booking/create-booking"),
                Method = HttpMethod.Post
            };

            bookingRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var bookingModel = new Dictionary<string, string>
            {
                { "LocationID", location.ID.ToString() },
                { "ApplicationUserId", individual.ID.ToString() },
                { "AvailableDateId", location.AvailableDates.First().ID.ToString() },
                { "TestType", "PCR" }
            };

            string bookingJson = JsonConvert.SerializeObject(bookingModel);

            bookingRequest.Content = new StringContent(bookingJson, System.Text.Encoding.UTF8, "application/json");

            await _client.SendAsync(bookingRequest);

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Booking/cancel-booking"),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "LocationID", location.ID.ToString() },
                { "ApplicationUserId", individual.ID.ToString() },
                { "AvailableDateId", location.AvailableDates.First().ID.ToString() },
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
        public async Task GetBooking_ReturnsSuccessMessages()
        {
            var request = BookingStatus.Pending;

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/Booking/get-booking?request=" + request),
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
