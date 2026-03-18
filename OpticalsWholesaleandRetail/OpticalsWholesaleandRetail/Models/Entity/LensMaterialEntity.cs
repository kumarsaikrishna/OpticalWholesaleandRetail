using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class LensMaterialEntity
    {
        [Key]
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
