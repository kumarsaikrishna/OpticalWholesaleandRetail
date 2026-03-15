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
namespace OpticalsWholesaleandRetail.Controllers
{
    public class FramesController : Controller
    {
        private readonly IFramesRepo _user;
        private readonly MyDbContext _context;

        public FramesController(IFramesRepo user,MyDbContext context)
        {
            _user = user;
            _context = context;
        }
        public IActionResult GetFrames()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var data = _user.GetFramesB(loggedInUser.userId);

            ViewBag.Brands = _context.fBrandEntity.Where(x => x.IsDeleted == false).ToList();
            //ViewBag.Models = _context.FrameModels.Where(x => x.IsDeleted == false).ToList();
            //ViewBag.Sizes = _context.FrameSizes.Where(x => x.IsDeleted == false).ToList();
            //ViewBag.Categories = _context.FrameCategories.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Suppliers = _context.suppliersEntity.Where(x => x.IsDeleted == false).ToList();

            return View(data);
        }
        //public IActionResult GetFrames(int userId)
        //{
        //    var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
        //    if (loggedInUser == null)
        //    {
        //        return RedirectToAction("Login", "Authenticate");
        //    }

        //    var res = _user.GetFrames(userId);
        //    return View(res);
        //}
        [HttpPost]
        public IActionResult AddFrameBrands(FrameBrandDto obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();

            response = _user.AddFrameBrands(obj, loggedInUser.userId);
            if (response.statuCode == 1)
            {
                return RedirectToAction("GetFrames");
            }
            else
            {
                return RedirectToAction("GetFrames");
            }
        }
        [HttpPost]
        public IActionResult UpdateFrameBrand(FrameBrandDto obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();

            response = _user.UpdateFrameBrand(obj, loggedInUser.userId);
            if (response.statuCode == 1)
            {
                return RedirectToAction("GetFrames");
            }
            else
            {
                return RedirectToAction("GetFrames");
            }
        }
        [HttpPost]
        public IActionResult DeleteFrameBrand(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();
            response = _user.DeleteFrameBrand(id);
            if (response.statuCode == 1)
            {
                return RedirectToAction("GetFrames");
            }
            else
            {
                return RedirectToAction("GetFrames");
            }
        }

        public IActionResult GetFrameModelById(int userId)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            ViewBag.Brands = _context.fBrandEntity.Where(x => x.IsDeleted == false).ToList();
            ViewBag.FrameSizes = _context.fSizeEntity.Where(x => x.IsDeleted == false).ToList();
            var res = _user.GetFrameModelsById(userId);
            return View(res);
        }
        [HttpPost]
        public IActionResult AddFrameModels(FrameModelDto obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();

            response = _user.AddFrameModels(obj, loggedInUser.userId);
            if (response.statuCode == 1)
            {
                return RedirectToAction("GetFrameModels");
            }
            else
            {
                return RedirectToAction("GetFrameModels");
            }
        }
        [HttpPost]
        public IActionResult UpdateFrameModels(FrameModelDto obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();

            response = _user.UpdateFrameModel(obj, loggedInUser.userId);
            if (response.statuCode == 1)
            {
                return RedirectToAction("GetFrames");
            }
            else
            {
                return RedirectToAction("GetFrames");
            }
        }
        [HttpPost]
        public IActionResult DeleteFrameModels(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();
            response = _user.DeleteFrameModel(id);
            if (response.statuCode == 1)
            {
                return RedirectToAction("GetFrames");
            }
            else
            {
                return RedirectToAction("GetFrames");
            }
        }

        public IActionResult GetFrameSizes()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var data = _user.GetFrameSizes();

            ViewBag.Supplier = _context.suppliersEntity
                              .Where(x => x.IsDeleted == false)
                              .ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult AddFrameSize(FrameSizeDto obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var result = _user.AddFrameSize(obj);

            return Json(new { success = result });
        }

        [HttpPost]
        public IActionResult UpdateFrameSize(FrameSizeDto obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var result = _user.UpdateFrameSize(obj);

            return Json(new { success = result });
        }

        [HttpPost]
        public IActionResult DeleteFrameSize(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var result = _user.DeleteFrameSize(id);

            return Json(new { success = result });
        }

        public IActionResult GetFrameCategories()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var data = _user.GetFrameCategories();

            ViewBag.Supplier = _context.suppliersEntity
                .Where(x => x.IsDeleted == false)
                .ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult AddFrameCategory(FrameCategoryDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var res = _user.AddFrameCategory(dto);

            return Json(new { success = res });
        }

        [HttpPost]
        public IActionResult UpdateFrameCategory(FrameCategoryDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var res = _user.UpdateFrameCategory(dto);

            return Json(new { success = res });
        }

        [HttpPost]
        public IActionResult DeleteFrameCategory(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var res = _user.DeleteFrameCategory(id);

            return Json(new { success = res });
        }

        public IActionResult GetFrame()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            ViewBag.Brands = _context.fBrandEntity.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Models = _context.fModelEntity.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Sizes = _context.fSizeEntity.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = _context.fCategoryEntities.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Suppliers = _context.suppliersEntity.Where(x => x.IsDeleted == false).ToList();

            var data = _user.GetFrames();

            return View(data);
        }

        [HttpPost]
        public IActionResult AddFrame(FrameDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var res = _user.AddFrame(dto);

            return Json(new { success = res });
        }

        [HttpPost]
        public IActionResult UpdateFrame(FrameDto dto)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var res = _user.UpdateFrame(dto);

            return Json(new { success = res });
        }

        [HttpPost]
        public IActionResult DeleteFrame(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            var res = _user.DeleteFrame(id);

            return Json(new { success = res });
        }


    }
}
