using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class FrameModelDto
    {
        public int ModelId { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ModelNumber { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
