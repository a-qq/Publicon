using Publicon.Core.Entities.Abstract;
using System.Collections.Generic;

namespace Publicon.Core.Entities.Concrete
{
    public class Category : Entity
    {
        private readonly List<Field> _fields = new List<Field>();
        private readonly List<Publication> _publications = new List<Publication>();

        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool IsArchived { get; protected set; }

        public virtual IReadOnlyCollection<Field> Fields => _fields.AsReadOnly();
        public virtual IReadOnlyCollection<Publication> Publications => _publications.AsReadOnly();

        public void AddPublication(Publication publication)
            => _publications.Add(publication);

        public void AddFields(IEnumerable<Field> fields)
            => _fields.AddRange(fields);

        public void AddField(Field field)
            => _fields.Add(field);

        public void DeleteField(Field field)
            => _fields.Remove(field);

        public void DeleteAllPublications()
            => _publications.Clear();
    }
}
