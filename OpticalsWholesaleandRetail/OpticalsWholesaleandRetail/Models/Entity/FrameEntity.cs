using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpticalsWholesaleandRetail.Models.Entity
{
    public class FrameEntity
    {[Key]
        public int FrameId { get; set; }

        public int? BrandId { get; set; }

        public int? ModelId { get; set; }

        public int? SizeId { get; set; }

        public int? CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? SellingPrice { get; set; }

        public int StockQty { get; set; }

        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
