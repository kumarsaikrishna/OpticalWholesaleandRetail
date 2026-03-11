using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class FrameCategoryEntity
    {[Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? SupplierId { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
