namespace Resorts.Frontend.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ResortInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string ResortId { get; set; }

        [JsonProperty(PropertyName = "resortName")]
        public string ResortName { get; set; }

        [JsonProperty(PropertyName = "resortDescription")]
        public string Description { get; set; }

        [JsonProperty("_self")] public string AltLink { get; set; }

        public List<string> Images { get; set; }
    }
}