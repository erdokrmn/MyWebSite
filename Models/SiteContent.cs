using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Models
{
    public class SiteContent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Key { get; set; } // Örnek: "AnaSayfa_ArkaPlan", "Hakkimda_Metin"

        [Required]
        public string Value { get; set; } // Metin, görsel URL, ya da link değeri

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } // "Text", "Image", "Link"
    }
}
