using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.DTO.Task;

namespace TaskManagement.IntegrationTests
{
    public class TaskControllerTests
    {
        private HttpClient _httpClient;

        private CustomWebApplicationFactory _webApplicationFactory;

        [SetUp]
        public void Setup()
        {
            _webApplicationFactory = new CustomWebApplicationFactory();
            _httpClient = _webApplicationFactory.CreateClient();
        }

        [Test]
        public async Task GetTasks()
        {
            var response = await _httpClient.GetAsync("/Task/Index");
            string result = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}