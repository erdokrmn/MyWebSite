namespace MyWebSite.Models.ViewModels
{
    public class SiteContentUploadViewModel
    {
        public string Key { get; set; }
        public string Type { get; set; } // "Image", "Text", "Link"
        public string? Value { get; set; } // Text or link
        public IFormFile? ImageFile { get; set; } // For image
    }
}
