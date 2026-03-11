using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class FrameSizeEntity
    {[Key]
        public int SizeId { get; set; }

        public string SizeName { get; set; }

        public int? SupplierId { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
