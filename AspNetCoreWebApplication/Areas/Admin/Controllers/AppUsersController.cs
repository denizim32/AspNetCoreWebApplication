using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUsersController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AppUsersController(DatabaseContext databaseContext) // Dependency Injection
        {
            _databaseContext = databaseContext;
        }

        // GET: AppUsersController
        public async Task<ActionResult> Index() // async ifadesi bu metodun asenkron çalışacağını ifade eder
        {
            return View(await _databaseContext.AppUsers.ToListAsync());
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create() // Get metotları sayfa ilk açıldığında çalışan metotlardır
        {
            return View(); // Eğer sayfa il açıldığında view a bir veri göndermemiz gerekirse bu blokta göndermeliyiz.
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _databaseContext.AppUsers.AddAsync(appUser);
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(appUser);
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            // Ampülden make method async yi seçiyoruz
            return View(await _databaseContext.AppUsers.FindAsync(id)); // FindAsync metodu kendisine verdiğimiz id ye sahip kaydı veritabanından bulup bize getirir
        }

        // POST: AppUsersController/Edit/5
        [HttpPost] // Aşağıdaki edit sadece sayfa post edilince çalışır
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _databaseContext.Entry(appUser).State = EntityState.Modified;
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(appUser);
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _databaseContext.AppUsers.FindAsync(id));
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, AppUser appUser)
        {
            try
            {
                _databaseContext.Entry(appUser).State = EntityState.Deleted;
                await _databaseContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
