using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace BizLand.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly BizLandContext _context;

        public SettingController(BizLandContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Settings.ToList());
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (setting is null) return NotFound();
            return View(setting);
        }

        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            if (!ModelState.IsValid) return View();
            Setting existSetting = _context.Settings.Find(setting.Id);
            if (setting is null) return NotFound();
            existSetting.Value = setting.Value;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
