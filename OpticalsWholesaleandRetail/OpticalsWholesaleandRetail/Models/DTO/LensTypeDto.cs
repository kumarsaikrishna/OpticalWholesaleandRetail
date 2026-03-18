using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class LensTypeDto
    {
        public int LensTypeId { get; set; }
        public string TypeName { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}
