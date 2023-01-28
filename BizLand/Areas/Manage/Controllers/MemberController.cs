using BizLand.DAL;
using BizLand.Helpers;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizLand.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MemberController : Controller
    {
        private readonly BizLandContext _context;
        private readonly IWebHostEnvironment _env;

        public MemberController(BizLandContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Members.Include(x => x.Position).ToList());
        }

        public IActionResult Create() 
        {
            ViewBag.Positions = _context.Positions.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            ViewBag.Positions = _context.Positions.ToList();
            if (!ModelState.IsValid) return View(member);
            if(member is null ) return NotFound();
            if (member.ImageFile is not null)
            {
                if (member.ImageFile.ContentType != "image/png" && member.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile" , "You can only upload files png or jpeg format");
                    return View();
                }

                if (member.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile" , "You can only upload files under 2mb size");
                    return View();
                }

                member.ImageUrl = member.ImageFile.SaveImage(_env.WebRootPath , "uploads/members");
            }

            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            ViewBag.Positions = _context.Positions.ToList();
            Member member = _context.Members.Find(id);
            if (member == null) return NotFound();
            return View(member);
        }


        [HttpPost]
        public IActionResult Edit(Member member)
        {
            ViewBag.Positions = _context.Positions.ToList();
            if (!ModelState.IsValid) return View(member);
            Member existMember = _context.Members.FirstOrDefault(x => x.Id == member.Id);
            if (existMember == null) return NotFound();
            if (member.ImageFile is not null)
            {
                if (member.ImageFile.ContentType != "image/png" && member.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "You can only upload files png or jpeg format");
                    return View();
                }

                if (member.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "You can only upload files under 2mb size");
                    return View();
                }
                string deletePath = Path.Combine(_env.WebRootPath , "uploads/members" , existMember.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

                existMember.ImageUrl = member.ImageFile.SaveImage(_env.WebRootPath, "uploads/members");
            }
            existMember.FirstName = member.FirstName;
            existMember.LastName = member.LastName;
            existMember.FacebookUrl= member.FacebookUrl;
            existMember.InstagramUrl = member.InstagramUrl;
            existMember.TwitterUrl= member.TwitterUrl;
            existMember.LinkedinUrl = member.LinkedinUrl;
            existMember.PositionId = member.PositionId;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            Member member = _context.Members.Find(id);
            if (member == null) return NotFound();
            string deletePath = Path.Combine(_env.WebRootPath, "uploads/members", member.ImageUrl);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            _context.Members.Remove(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
