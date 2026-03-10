using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class OrdersDto
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public int SupplierId { get; set; }

        public string Status { get; set; }
        public string SupplierName { get; set; }

        public decimal TotalAmount { get; set; }

        public int CreatedBy { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
