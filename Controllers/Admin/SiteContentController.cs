using Microsoft.AspNetCore.Mvc;
using MyWebSite.Data;
using MyWebSite.Models;

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
            return View(contents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SiteContent model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    model.ImagePath = "/images/" + fileName;
                }

                _context.SiteContents.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public IActionResult Edit(Guid id)
        {
            var content = _context.SiteContents.FirstOrDefault(x => x.Id == id);
            if (content == null)
                return NotFound();
            return View(content);
        }

        [HttpPost]
        public IActionResult Edit(SiteContent model, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = _context.SiteContents.FirstOrDefault(x => x.Id == model.Id);
            if (existing == null)
                return NotFound();

            // Metin alanlarını güncelle
            existing.Title = model.Title;
            existing.Content = model.Content;

            // Sadece yeni resim gelirse güncelle
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                existing.ImagePath = "/images/" + fileName;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Delete(Guid id)
        {
            var content = _context.SiteContents.FirstOrDefault(x => x.Id == id);
            if (content != null)
            {
                _context.SiteContents.Remove(content);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
