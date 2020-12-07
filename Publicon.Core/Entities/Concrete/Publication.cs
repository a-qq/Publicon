using Publicon.Core.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Publicon.Core.Entities.Concrete
{
    public class Publication : Entity
    {
        private readonly List<PublicationField> _publicationFields = new List<PublicationField>();

        public string Title { get; protected set; }
        public DateTime PublicationTime { get; protected set; }
        public string Description { get; protected set; }
        public string FileName { get; protected set; }
        public DateTime AddedAt { get; protected set; }

        public Guid? UserId { get; protected set; }
        public virtual User User { get; protected set; }

        public Guid CategoryId { get; protected set; }
        public virtual Category Category { get; protected set; }
        public virtual IReadOnlyCollection<PublicationField> PublicationFields => _publicationFields.AsReadOnly();

        public void SetFileName(string fileName)
            => FileName = fileName;

        public void SetUser(User user)
            => User = user;

        public void AddPublicationFields(IEnumerable<PublicationField> fields)
            => _publicationFields.AddRange(fields);

        public void AddPublicationField(PublicationField field)
            => _publicationFields.Add(field);

        public void DeletePublicationField(PublicationField field)
            => _publicationFields.Remove(field);
    }
}
