using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class FrameSizeDto
    {
        public int SizeId { get; set; }

        public string SizeName { get; set; }

        public int? SupplierId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
