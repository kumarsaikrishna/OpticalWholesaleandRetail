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
        
        public FramesController( IFramesRepo user)
        {
            _user = user;
        }
        public IActionResult GetFrames(int userId)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var res = _user.GetFrames(userId);
            return View(res);
        }
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

    }
}
