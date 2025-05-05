using Microsoft.AspNetCore.Mvc;
using MyWebSite.Data;
using MyWebSite.Models.ViewModels;

namespace MyWebSite.Controllers
{
    public class MainPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MainPage()
        {
            var contents = _context.SiteContents.ToList();

            ViewBag.AnaSayfaBg = contents.FirstOrDefault(x => x.Key == "AnaSayfa_ArkaPlan")?.Value;
            ViewBag.HakkimdaBg = contents.FirstOrDefault(x => x.Key == "Hakkimda_ArkaPlan")?.Value;
            ViewBag.HakkimdaYazi = contents.FirstOrDefault(x => x.Key == "Hakkimda_Metin")?.Value;

            ViewBag.SocialLinks = contents
                .Where(x => x.Type == "Link" && x.Key.StartsWith("fab")) // örn: fab fa-twitter
                .ToList();

            return View();
        }
    }
}
