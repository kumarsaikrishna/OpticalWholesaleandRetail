using Microsoft.EntityFrameworkCore;
using OpticalFibersRetailShop.Models.Entity;
using OpticalFibersRetailShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using OpticalFibersRetailShop.DAL;
using OpticalFibersRetailShop.Models.DTO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace SmartSchool.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserRepo _user;
        private readonly MyDbContext _context;
        public UserManagementController(MyDbContext context,IUserRepo user)
        {
            _user = user;
            _context = context;
        }
        [Authorize(Policy = "Super Admin, Admin")]
        public IActionResult GetUserType()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
           
            var res = _user.GetUserType(loggedInUser.userId);
            return View(res);
        }
        [HttpPost]
        public IActionResult AddUserType(UserTypeDTO obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();

            response = _user.AddUserType(obj,loggedInUser.userId);
            if (response.statuCode == 1)
            {
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("GetUserType");
            }
        }
        [HttpPost]
        public IActionResult UpdateUserType(UserTypeDTO obj)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();

            response = _user.UpdateUserType(obj, loggedInUser.userId);
            if (response.statuCode == 1)
            {
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("GetUserType");
            }
        }
        [HttpPost]
        public IActionResult DeleteUserType(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();
            response = _user.DeleteUserType(id);
            if (response.statuCode == 1)
            {
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("GetUserType");
            }
        }

        //UserMaster
        public IActionResult GetUser(string? state )
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
         
            var usertype = _context.uTypeEntity.Where(a => a.IsDeleted == false ).Select(a => a.RoleName).ToList();
            ViewBag.UserType = usertype;
            var res = _user.GetUser(loggedInUser.userId);
            return View(res);
        }
        public IActionResult AddUser(IFormCollection form)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
                return RedirectToAction("Login", "Authenticate");

            var user = new UserDto
            {
                FullName = form["FullName"],
                StoreName=form["StoreName"],
                Email = form["Email"],
                UserType = form["UserType"],
                PasswordHash = form["PasswordHash"],
                AddressLine = form["AddressLine"],
              Phone=form["Phone"],
            };

            var result = _user.AddUser(user, loggedInUser.userId);
            bool success = result != null;

            return Json(new
            {
                success = success,
                message = success ? "User added successfully" : "User could not be added"
            });
        }

        [HttpPost]
        public IActionResult UpdateUser(IFormCollection form)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var email = form["Email"].ToString().Trim();

            if (!Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
            {
                return Json(new { success = false, message = "Enter a valid Email Id" });
            }

            var user = new UserDto
            {
                UserId = Convert.ToInt32(form["UserId"]),
                FullName = form["FullName"],
                StoreName=form["StoreName"],
                Email = email,
                UserType = form["UserType"],
                PasswordHash = form["PasswordHash"],
                AddressLine=form["AddressLine"],
                Phone=form["Phone"],
            };

           

            var result = _user.UpdateUser(user, loggedInUser.userId);

            if (result != null && result.statuCode == 1)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result?.message ?? "User could not be updated" });
            }
        }
        public IActionResult DeleteUser(int id)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();
            response = _user.DeleteUser(id);
            if (response.statuCode == 1)
            {
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("GetUser");
            }
        }

    }
}
