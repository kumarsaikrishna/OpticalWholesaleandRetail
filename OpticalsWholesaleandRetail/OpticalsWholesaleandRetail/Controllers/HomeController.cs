using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpticalFibersRetailShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OpticalsWholesaleandRetail.DAL;
using OpticalsWholesaleandRetail.Utilities;
using OpticalsWholesaleandRetail.Models.Entity;

namespace OpticalsWholesaleandRetail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepo _service;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, IUserRepo service, MyDbContext context)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {

            ViewBag.TotalSubscriptions = _service.TotalSubscriptions();

            return View();
        }
        public IActionResult AdminDashboard()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            ViewBag.TotalSubscriptions = _service.TotalSubscriptions();
            ViewBag.UserName = _context.userEntity.Where(a => a.UserId == loggedInUser.userId && a.IsDeleted == false).Select(a => a.FullName).FirstOrDefault();
            //int school = _context.userEntity.Where(a => a.UserId == loggedInUser.userId && a.IsDeleted == false).Select(a => a.SchoolId).FirstOrDefault();

            int sid = _context.uTypeEntity.Where(a => a.RoleName == "Supplier" && a.IsDeleted == false).Select(a => a.RoleId).FirstOrDefault();
            int tid = _context.uTypeEntity.Where(a => a.RoleName == "Customer" && a.IsDeleted == false).Select(a => a.RoleId).FirstOrDefault();
            ViewBag.TotalSuppliers = _context.userEntity.Where(a => a.RoleId == tid && a.IsDeleted == false).Count();
            ViewBag.TotalCustomers = _context.userEntity.Where(a => a.RoleId == sid && a.IsDeleted == false).Count();
            //ViewBag.TotalFee = _context.studentEntity.Where(f => f.IsDeleted == false && f.SchoolId == school).Sum(f => f.TotalFee);
            //ViewBag.CollectedFee = _context.feePaymentEntity.Where(f => f.IsDeleted == false && f.CreatedBy == loggedInUser.userId).Sum(f => f.Amount);
            ViewBag.Name = _context.userEntity.Where(a => a.UserId == loggedInUser.userId && a.IsDeleted == false).Select(a => a.FullName).FirstOrDefault();

            //ViewBag.Profile = _context.userEntity.Where(a => a.UserId == loggedInUser.userId && a.IsDeleted == false).Select(a => a.ProfilePicture).FirstOrDefault();
            

            return View();
        }
        public IActionResult TeacherDashboard()
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            ViewBag.TotalSubscriptions = _service.TotalSubscriptions();
            ViewBag.Name = _context.userEntity.Where(a => a.UserId == loggedInUser.userId && a.IsDeleted == false).Select(a => a.FullName).FirstOrDefault();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TableCopy()
        {
            return View();
        }


        //[HttpGet]
        //public IActionResult GetSubscriptions()
        //{
        //    try
        //    {
        //        var subscriptions = _service.GetSubscriptions(); // Call your service here
        //        return Json(new { success = true, data = subscriptions });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}



    }
}
