using System;

namespace Publicon.Core.Entities.Abstract
{
    public abstract class DeleteAble : Entity
    {
        public DateTime? DeletedAt { get; protected set; }
    }
}
