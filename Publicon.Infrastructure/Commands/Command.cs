using System;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Commands
{
    public class Command
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
