using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tionit.RabbitMQ.Producer
{
    public class MessageModel
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
