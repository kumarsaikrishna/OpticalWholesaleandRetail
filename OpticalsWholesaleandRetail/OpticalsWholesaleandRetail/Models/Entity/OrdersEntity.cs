using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class OrdersEntity
    {[Key]
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public int? CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public int? SupplierId { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public int? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
