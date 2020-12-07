using Publicon.Core.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Publicon.Core.Entities.Concrete
{
    public class Field : Entity
    {
        private readonly List<PublicationField> _publicationFields = new List<PublicationField>();

        public string Name { get; protected set; }
        public bool IsRequired { get; protected set; }

        public Guid CategoryId { get; protected set; }
        public virtual Category Category { get; protected set; }

        public virtual IReadOnlyCollection<PublicationField> PublicationFields => _publicationFields.AsReadOnly();
    }
}
