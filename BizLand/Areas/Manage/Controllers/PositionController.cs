using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace BizLand.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        private readonly BizLandContext _context;

        public PositionController(BizLandContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Positions.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid) return View();
            if (position is null) return NotFound();
            _context.Positions.Add(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Position position = _context.Positions.Find(id);
            if (position is null) return NotFound();
            return View(position);
        }

        [HttpPost]
        public IActionResult Edit(Position position)
        {
            if (!ModelState.IsValid) return View();
            Position existPosition = _context.Positions.FirstOrDefault(x => x.Id == position.Id);
            if (existPosition is null) return NotFound();
            existPosition.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Position position = _context.Positions.Find(id);
            if (position is null) return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
