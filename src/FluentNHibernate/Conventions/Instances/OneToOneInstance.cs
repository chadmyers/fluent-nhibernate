using System;
using System.Reflection;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Instances
{
    public class OneToOneInstance : IOneToOneInstance
    {
        private readonly OneToOneMapping mapping;
        private bool nextBool = true;

        public OneToOneInstance(OneToOneMapping mapping)
        {
            this.mapping = mapping;
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Name; }
        }

        public bool IsSet(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.IsSpecified(x => x.Access))
                        mapping.Access = value;
                });
            }
        }

        public ICascadeInstance Cascade
        {
            get
            {
                return new CascadeInstance(value =>
                {
                    if (!mapping.IsSpecified(x => x.Cascade))
                        mapping.Cascade = value;
                });
            }
        }

        public IOneToOneInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public IFetchInstance Fetch
        {
            get
            {
                return new FetchInstance(value =>
                {
                    if (!mapping.IsSpecified(x => x.Fetch))
                        mapping.Fetch = value;
                });
            }
        }

        public IOuterJoinInstance OuterJoin
        {
            get
            {
                return new OuterJoinInstance(value =>
                {
                    if (!mapping.IsSpecified(x => x.OuterJoin))
                        mapping.OuterJoin = value;
                });
            }
        }

        public void Class<T>()
        {
            if (!mapping.IsSpecified(x => x.Class))
                mapping.Class = new TypeReference(typeof(T));
        }

        public void Class(Type type)
        {
            if (!mapping.IsSpecified(x => x.Class))
                mapping.Class = new TypeReference(type);
        }

        public void Constrained()
        {
            if (mapping.IsSpecified(x => x.Constrained))
                return;

            mapping.Constrained = nextBool;
            nextBool = true;
        }

        public void ForeignKey(string key)
        {
            if (!mapping.IsSpecified(x => x.ForeignKey))
                mapping.ForeignKey = key;
        }

        public void LazyLoad()
        {
            if (mapping.IsSpecified(x => x.Lazy))
                return;

            mapping.Lazy = nextBool ? Laziness.True : Laziness.False;
            nextBool = true;
        }

        public void PropertyRef(string propertyName)
        {
            if (!mapping.IsSpecified(x => x.PropertyRef))
                mapping.PropertyRef = propertyName;
        }
    }
}