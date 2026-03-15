using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class FrameDto
    {
        public int FrameId { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int ModelId { get; set; }
        public string ModelNumber { get; set; }

        //public int SizeId { get; set; }
        //public string SizeName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
