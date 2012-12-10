
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using InMemoryHostWithNinject.Web.Api.Models;
using NUnit.Framework;
using Newtonsoft.Json;

namespace InMemoryHostWithNinject.Web.Api.Tests.Controllers
{
    [TestFixture]
    public class SandwichControllerTest : BaseControllerTest
    {

        [Test]
        [Description("This test attempts to retrieve a known sandwich based on an explicit id.")]
        public void Get_Sandwich_With_SandwichId_Success()
        {
            var client = new HttpClient(_httpServer);
            const int sandwichId = 1;

            var request = CreateRequest(string.Format("api/sandwich/{0}", sandwichId), "application/json", HttpMethod.Get);
            using (HttpResponseMessage response = client.SendAsync(request, new CancellationTokenSource().Token).Result)
            {
                Assert.NotNull(response.Content);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);

                var result = response.Content.ReadAsAsync<ISandwich>().Result;
                Assert.NotNull(result);

                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("Deli Sandwich", result.Description);                               
                Assert.That(response.IsSuccessStatusCode);
            }

            request.Dispose();

        }

        [Test]
        [Description("This test attempts to create a new valid sandwich.")]
        public void Create_Sandwich_Success()
        {
            var client = new HttpClient(_httpServer);

            var postAddress = string.Format("api/sandwich/");

            client.BaseAddress = new Uri(Url);

            var sandwich = new DeliSandwich() { Description = "Egg Salad Sandwich", Id = 2}; // <---- Note that this is an explicit object, not an interface
            var postData = new StringContent(JsonConvert.SerializeObject(sandwich), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = client.PostAsync(postAddress, postData, new CancellationTokenSource().Token).Result)
            {
                Assert.That(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            } 
        }

        [Test]
        [Description("This test ensures that if nothing is sent to the controller, a BadRequest is returned.")]
        public void Create_Sandwich_Null_Data_Failure()
        {
            var client = new HttpClient(_httpServer);

            var postAddress = string.Format("api/sandwich/");

            client.BaseAddress = new Uri(Url);
           
            using (HttpResponseMessage response = client.PostAsync(postAddress, null, new CancellationTokenSource().Token).Result)
            {
                Assert.IsFalse(response.IsSuccessStatusCode);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            } 
        }
    }
}
