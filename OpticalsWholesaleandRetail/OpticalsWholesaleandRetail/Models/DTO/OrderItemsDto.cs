using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class OrderItemsDto
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public string ProductType { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
