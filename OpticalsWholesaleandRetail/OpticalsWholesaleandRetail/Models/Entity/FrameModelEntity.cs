using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class FrameModelEntity
    {[Key]
        public int ModelId { get; set; }

        public int BrandId { get; set; }

        public string ModelNumber { get; set; }

        public bool? IsDeleted { get; set; }
        public int CreatedBy { get; set; }
    }
}
