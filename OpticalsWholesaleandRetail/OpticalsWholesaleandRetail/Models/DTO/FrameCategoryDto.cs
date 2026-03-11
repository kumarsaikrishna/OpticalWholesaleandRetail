using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class FrameCategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? SupplierId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
