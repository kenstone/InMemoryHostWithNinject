using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InMemoryHostWithNinject.Web.Api.Models
{
    public class DeliSandwich : ISandwich
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}