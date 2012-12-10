using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using InMemoryHostWithNinject.Web.Api.App_Start;
using NUnit.Framework;

namespace InMemoryHostWithNinject.Web.Api.Tests.Controllers
{
    [TestFixture]
    public abstract class BaseControllerTest : IDisposable
    {
        protected HttpServer _httpServer;
        protected const string Url = "http://notebookheavy.com/";

        [TestFixtureSetUp]
        public void SetupHttpServer()
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{id}/{action}", defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional });

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            NinjectWebCommon.Start();

            var resolver = new NinjectResolver(NinjectWebCommon.bootstrapper.Kernel);
            config.DependencyResolver = resolver;

            _httpServer = new HttpServer(config);
        }

        public void Dispose()
        {
            if (_httpServer != null)
            {
                _httpServer.Dispose();
            }
        }

        /// <summary>
        /// This method is taken from Filip W in a blog post located at: http://www.strathweb.com/2012/06/asp-net-web-api-integration-testing-with-in-memory-hosting/
        /// </summary>
        /// <param name="url"></param>
        /// <param name="mthv"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected HttpRequestMessage CreateRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Url + url)
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

    }
}
