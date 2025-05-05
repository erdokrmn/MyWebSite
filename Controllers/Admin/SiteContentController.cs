using Microsoft.AspNetCore.Mvc;
using MyWebSite.Data;
using MyWebSite.Models;
using MyWebSite.Models.ViewModels;

namespace MyWebSite.Controllers.Admin
{
    public class SiteContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiteContentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contents = _context.SiteContents.ToList();
            ViewBag.Contents = contents;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateContent(SiteContentUploadViewModel model)
        {
            string value = model.Value;

            if (model.Type == "Image" && model.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                value = "/uploads/" + fileName;
            }

            var existing = _context.SiteContents.FirstOrDefault(c => c.Key == model.Key);
            if (existing == null)
            {
                var newItem = new SiteContent
                {
                    Key = model.Key,
                    Type = model.Type,
                    Value = value
                };
                _context.SiteContents.Add(newItem);
            }
            else
            {
                existing.Value = value;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Sosyal medya linkini silmek için (isteğe bağlı)
        [HttpPost]
        public IActionResult DeleteContent(string key)
        {
            var content = _context.SiteContents.FirstOrDefault(x => x.Key == key);
            if (content != null)
            {
                _context.SiteContents.Remove(content);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
