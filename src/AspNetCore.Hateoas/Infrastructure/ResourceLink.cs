using Microsoft.AspNetCore.Routing;
using System;

namespace AspNetCore.Hateoas.Infrastructure
{
    public class ResourceLink<T> : ILinksRequirement
    {
        public ResourceLink(Type resourceType, string name, Func<T, RouteValueDictionary> values)
        {
            this.ResourceType = resourceType;
            this.Name = name;
            this.Values = values;
        }

        public string Name { get; }
        public Func<T, RouteValueDictionary> Values { get; }
        public Type ResourceType { get; }

        public object RouteValues(object input)
        {
            return Values((T)input);
        }
    }
}
