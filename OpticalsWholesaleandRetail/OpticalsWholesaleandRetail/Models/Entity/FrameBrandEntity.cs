using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class FrameBrandEntity
    {[Key]
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public string Category { get; set; }

        public int? SupplierId { get; set; }

        public bool? IsDeleted { get; set; }

        
    }
}
