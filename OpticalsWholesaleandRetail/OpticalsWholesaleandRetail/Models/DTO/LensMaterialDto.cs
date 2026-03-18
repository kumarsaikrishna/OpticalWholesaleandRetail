using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class LensMaterialDto
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}
