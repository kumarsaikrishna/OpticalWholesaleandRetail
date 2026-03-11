using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class FrameBrandDto
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public string Category { get; set; }

        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
