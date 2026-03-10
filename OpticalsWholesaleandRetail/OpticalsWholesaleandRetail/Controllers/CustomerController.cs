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
    public class CustomerController : Controller
    {
        private readonly IUserRepo _user;
        private readonly MyDbContext _context;
        public CustomerController(MyDbContext context, IUserRepo user)
        {
            _user = user;
            _context = context;
        }
        public IActionResult UserStores(int UserId)
        {
            var stores = _user.UserStores(UserId);

            ViewBag.CustomerId = _context.userEntity.Where(a=>a.UserId== UserId && a.IsDeleted==false).Select(a=>a.UserId).FirstOrDefault();
            ViewBag.CustomerName = _context.userEntity.Where(a => a.UserId == UserId && a.IsDeleted == false).Select(a => a.FullName).FirstOrDefault();
            return View(stores);
        }
        public IActionResult AddStore(int customerId)
        {
            var model = new CustomersDto();
            model.CustomersId = customerId;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddStore(CustomersDto model)
        {
            var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            GenericResponse response = new GenericResponse();
            var store = new Customer
            {
                StoreName = model.StoreName,
                Email=model.Email,
                Country=model.Country,
                City = model.City,
                Phone = model.Phone,
                Address = model.Address,
                CustomersId = model.CustomerId,
                IsActive=true,
                CreatedBy=loggedInUser.userId,
                CreatedDate=DateTime.Now,
                IsDeleted=false
            };

            _context.customerEntity.Add(store);
            _context.SaveChanges();

            return RedirectToAction("CustomerStores",
                new { customerId = model.CustomerId });
        }

        public IActionResult EditStore(int id)
        {
            var store = _context.customerEntity.Find(id);

            var dto = new Customer
            {
                CustomerId = store.CustomerId,
                StoreName = store.StoreName,
                City = store.City,
                Phone = store.Phone,
                Address = store.Address,
                Email = store.Email,
                Country = store.Country,
                CustomersId = store.CustomersId
            };

            return View(dto);
        }
        //public IActionResult EditStore(CustomersDto obj)
        //{
        //    var loggedInUser = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "loggedUser");
        //    if (loggedInUser == null)
        //    {
        //        return RedirectToAction("Login", "Authenticate");
        //    }
        //    GenericResponse response = new GenericResponse();

        //    response = _context.(obj, loggedInUser.userId);
        //    if (response.statuCode == 1)
        //    {
        //        return RedirectToAction("UserStores");
        //    }
        //    else
        //    {
        //        return RedirectToAction("UserStores");
        //    }
        //}
        public IActionResult DeleteStore(int id)
        {
            var store = _context.customerEntity.Find(id);
            store.IsActive = true;
            store.IsDeleted = false;
            _context.customerEntity.Remove(store);
            _context.SaveChanges();

            return RedirectToAction("UserStores");
        }
    }
}
