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
            var model = new MainPageViewModel
            {
                Home = _context.SiteContents.Where(x => x.Page == "Home").ToList(),
                About = _context.SiteContents.Where(x => x.Page == "About").ToList(),
                Resume = _context.SiteContents.Where(x => x.Page == "Resume").ToList(),
                Portfolio = _context.SiteContents.Where(x => x.Page == "Portfolio").ToList()
            };

            return View(model);
        }
    }
}
