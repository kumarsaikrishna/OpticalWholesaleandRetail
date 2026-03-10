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
    public class OrdersController : Controller
    {
        private readonly IUserRepo _user;
        private readonly MyDbContext _context;
        public OrdersController(MyDbContext context, IUserRepo user)
        {
            _user = user;
            _context = context;
        }
        public IActionResult GetOrdersByStore(int storeId)
        {
            var orders = _context.ordersEntities
                .Where(x => x.CustomerId == storeId)
                .Select(x => new
                {
                    orderId = x.OrderId,
                    OrderNumber = x.OrderNumber,
                    OrderDate=x.OrderDate,
                    SupplierName = _context.userEntity.Where(a => a.UserId == x.SupplierId).Select(a => a.FullName).FirstOrDefault(),
                    quantity = _context.OrderItemEntities.Where(a=>a.OrderId==x.OrderId).Select(a=>a.Quantity).FirstOrDefault(),
                  
                    totalAmount = x.TotalAmount,
                    orderDate = x.OrderDate.ToString("dd-MM-yyyy")
                })
                .ToList();

            return Json(orders);
        }
    }
}
