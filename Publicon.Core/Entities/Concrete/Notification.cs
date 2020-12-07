using Publicon.Core.Entities.Abstract;
using System;

namespace Publicon.Core.Entities.Concrete
{
    public class Notification : Entity
    {
        public string Title { get; protected set; }
        public string Message { get; protected set; }
        public DateTime SendTime { get; protected set; }
    }
}
