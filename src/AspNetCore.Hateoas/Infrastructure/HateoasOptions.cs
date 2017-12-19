using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace AspNetCore.Hateoas.Infrastructure
{
    public class HateoasOptions
    {
        private readonly List<ILinksRequirement> links;

        public HateoasOptions()
        {
            links = new List<ILinksRequirement>();

        }
        public IReadOnlyList<ILinksRequirement> Requirements
        {
            get
            {
                return links.AsReadOnly();
            }
        }

        public HateoasOptions AddLink<T>(string routeName, Func<T, object> values) where T : class
        {
            Func<T, RouteValueDictionary> getRouteValues = r => new RouteValueDictionary();
            if (values != null)
            {
                getRouteValues = r => new RouteValueDictionary(values(r));
            }
                
            var req = new ResourceLink<T>(typeof(T), routeName, getRouteValues);

            links.Add(req);
            return this;
        }

        public HateoasOptions AddLink<T>(string routeName) where T : class
        {
            return AddLink<T>(routeName, t => null);
        }
    }

}
