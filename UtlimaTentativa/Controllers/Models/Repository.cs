namespace UtlimaTentativa.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string HtmlUrl { get; set; }
        public List<Contributor> Contributors { get; set; }
    }
}