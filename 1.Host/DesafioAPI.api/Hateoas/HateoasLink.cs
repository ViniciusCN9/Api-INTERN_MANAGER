namespace DesafioAPI.api.Hateoas
{
    public class HateoasLink
    {   
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public HateoasLink(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}