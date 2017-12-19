namespace AspNetCore.Hateoas.Models
{
    public class Link
    {
        public Link(string rel, string href, string method)
        {
            this.Rel = rel;
            this.Href = href;
            this.Method = method;
        }
        public string Href { get; }
        public string Rel { get; }
        public string Method { get; }

    }
}
