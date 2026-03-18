using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class LensTypeEntity
    {
        [Key]
        public int LensTypeId { get; set; }
        public string TypeName { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
