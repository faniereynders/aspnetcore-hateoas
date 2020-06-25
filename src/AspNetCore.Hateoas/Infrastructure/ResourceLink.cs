using Microsoft.AspNetCore.Routing;
using System;

namespace AspNetCore.Hateoas.Infrastructure
{
    public class ResourceLink<T> : ILinksRequirement
    {
        public ResourceLink(Type resourceType, string name, Func<T, RouteValueDictionary> values, Func<T, bool> condition)
        {
            this.ResourceType = resourceType;
            this.Name = name;
            this.Values = values;
            this.Condition = condition;
        }

        public string Name { get; }
        public Func<T, RouteValueDictionary> Values { get; }
        public Type ResourceType { get; }
        public Func<T, bool> Condition { get; }

        public object RouteValues(object input)
        {
            return Values((T)input);
        }

        public bool IsEnabled(object input)
        {
            if (input is T t)
            {
                return Condition(t);
            }
            else
            {
                return true;
            }
        }
    }
}
