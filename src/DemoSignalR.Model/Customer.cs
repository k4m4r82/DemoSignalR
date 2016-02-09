using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DemoSignalR.Model
{
    public class Customer
    {
        [JsonProperty("CustomerId")]
        public string CustomerId { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("ContactName")]
        public string ContactName { get; set; }
    }
}
