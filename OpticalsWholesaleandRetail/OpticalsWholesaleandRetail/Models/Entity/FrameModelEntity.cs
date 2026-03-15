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

        public int FrameSizeId { get; set; }

        public string Color { get; set; }

        public string Material { get; set; }

        public string FrameType { get; set; }   // Full Rim / Half Rim / Rimless

        public string GenderType { get; set; }  // Men / Women / Unisex / Kids

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int StockQuantity { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
