using System.Net;
using System.Net.Http;
using System.Web.Http;
using InMemoryHostWithNinject.Web.Api.Models;

namespace InMemoryHostWithNinject.Web.Api.Controllers
{
    public class SandwichController : ApiController
    {
        private readonly ISandwich _sandwich;

        public SandwichController(ISandwich sandwich)
        {
            // Totally contrived. Why would I do this? I wouldn't. I just needed something to demonstrate Ninject IoC working.
            sandwich.Description = "Deli Sandwich";
            sandwich.Id = 1;

            _sandwich = sandwich;
        }

        // GET api/sandwich/5
        public ISandwich Get(int id)
        {
            return _sandwich;
        }

        // POST api/sandwich
        public HttpResponseMessage Post(DeliSandwich sandwich) // <-- Note that this is an explicit class, not an interface
        {   
            // Make sure we've got something. Throw a bad request if there's nothing in it.
            if (sandwich == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "sandwich cannot be null"));
            }

            var mySandwich = new DeliSandwich() {Description = sandwich.Description, Id = sandwich.Id};
            
            // Do something with it....I'm not going to do anything with it. You get the picture.

            return new HttpResponseMessage(HttpStatusCode.Created);

        }

    }
}
