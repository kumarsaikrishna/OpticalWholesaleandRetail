using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class LensCoatingEntity
    {
        [Key]
        public int CoatingId { get; set; }
        public string CoatingName { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
