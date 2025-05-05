namespace MyWebSite.Models
{
    public class SocialLink
    {
        public int Id { get; set; }
        public string Platform { get; set; } // Örn: Twitter, Instagram
        public string Url { get; set; }      // Örn: https://twitter.com/erdokrmn
        public string IconClass { get; set; } // Örn: fab fa-twitter
    }

}
