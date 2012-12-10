using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InMemoryHostWithNinject.Web.Api.Models
{
    public interface ISandwich
    {
        int Id { get; set; }
        string Description { get; set; }
    }
}
