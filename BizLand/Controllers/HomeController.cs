
using BizLand.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BizLand.Controllers
{
    public class HomeController : Controller
    {
        private readonly BizLandContext _context;

        public HomeController(BizLandContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Members.Include(x => x.Position).ToList());
        }

    }
}