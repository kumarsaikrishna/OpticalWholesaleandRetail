using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class LensTintEntity
    {
        [Key]
        public int TintId { get; set; }
        public string TintName { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
