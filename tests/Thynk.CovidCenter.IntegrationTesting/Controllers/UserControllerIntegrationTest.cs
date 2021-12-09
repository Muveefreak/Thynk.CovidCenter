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
    public class UserControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CovidCenterDbContext covidCenterDb { get; set; }
        public UserControllerIntegrationTest(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            covidCenterDb = factory.covidCenterDb;
        }

        [Fact]
        public async Task CreateUser_ValidModel_ReturnsSuccessMessages()
        {
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/User/create-user"),
                Method = HttpMethod.Post
            };

            postRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestModel = new Dictionary<string, string>
            {
                { "UserName", "New Employee" },
                { "ID", "45181B6C-D7D5-46CC-CCB4-08D9BA97673B" },
                { "Email", "25" },
                { "Password", "25" },
                { "UserRole", "Administrator" }
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
        public async Task GetUsers_ReturnsSuccessMessages()
        {
            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/User/get-users"),
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
        public async Task GetUser_ValidModel_ReturnsSuccessMessages()
        {
            var individual = covidCenterDb.Set<ApplicationUser>().Where(x => x.UserRole == UserRole.Administrator).FirstOrDefault();

            var postRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/api/User/get-user?userId="+ individual.ID),
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
