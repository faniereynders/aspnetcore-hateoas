using System;

namespace AspNetCore.Hateoas.Infrastructure
{
    public interface ILinksRequirement
    {
        string Name { get; }
        object RouteValues(object input);
        Type ResourceType { get; }
    }
}

