using Publicon.Core.Entities.Abstract;
using System;

namespace Publicon.Core.Entities.Concrete
{
    public class PublicationField : Entity
    {
        public string Value { get; protected set; }

        public virtual Field Field { get; protected set; }
        public Guid FieldId { get; protected set; }

        public virtual Publication Publication { get; protected set; }
        public Guid PublicationId { get; protected set; }

        public PublicationField(Guid fieldId, string value)
        {
            FieldId = fieldId;
            Value = value;
        }
    }
}
