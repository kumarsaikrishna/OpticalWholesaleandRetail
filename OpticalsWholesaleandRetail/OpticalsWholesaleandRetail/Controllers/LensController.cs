using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticalsWholesaleandRetail.Models.Entity;
using OpticalsWholesaleandRetail.Utilities;
using Microsoft.AspNetCore.Mvc;
using OpticalsWholesaleandRetail.DAL;
using OpticalsWholesaleandRetail.Models.DTO;
using System.Text.RegularExpressions;

namespace OpticalsWholesaleandRetail.Controllers
{
    public class LensController : Controller
    {
        private readonly ILensRepo _repo;
        private readonly MyDbContext _context;

        public LensController(ILensRepo repo, MyDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public IActionResult GetLensType()
        {
            var data = _repo.GetLensTypes();

            ViewBag.Supplier = _context.suppliersEntity
                .Where(x => x.IsDeleted == false)
                .ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Add(LensTypeDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _repo.AddLensType(dto);
            return Json(res);
        }

        [HttpPost]
        public IActionResult Update(LensTypeDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _repo.UpdateLensType(dto);
            return Json(res);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _repo.DeleteLensType(id);
            return Json(res);
        }
        public IActionResult GetLensCategory()
        {
            ViewBag.Supplier = _context.suppliersEntity.Where(x => x.IsDeleted == false).ToList();
            return View(_repo.GetLensCategories());
        }

        [HttpPost]
        public IActionResult AddCategory(LensCategoryDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _repo.Add(dto);
            return Json(res);
        }
        [HttpPost]
        public IActionResult UpdateCategory(LensCategoryDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _repo.Update(dto);
            return Json(res);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _repo.Delete(id);
            return Json(res);
        }

    }
}
