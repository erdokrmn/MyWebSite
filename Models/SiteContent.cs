using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Models
{
    public class SiteContent
    {
        public Guid Id { get; set; }

        [Required]
        public string Page { get; set; }  // Örn: About, Resume

        [Required]
        public string Section { get; set; } // Örn: SoftwareSkills, Experience

        public string Title { get; set; }   // Örn: "About", "Experience", "Photoshop"
        public string Content { get; set; } // Açıklama metni
        public string ImagePath { get; set; } // Görsel yolu (varsa)
    }
}
