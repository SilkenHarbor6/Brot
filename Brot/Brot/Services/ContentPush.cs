using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brot.Services
{

    [JsonObject]
    public class ContentPush
    {
        [JsonProperty("custom_data")]
        public IDictionary<string, string> CustomData { get; set; }
    }

}
