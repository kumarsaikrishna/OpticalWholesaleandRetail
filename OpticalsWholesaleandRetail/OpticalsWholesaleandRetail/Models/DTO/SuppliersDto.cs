using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticalsWholesaleandRetail.Models.DTO
{
    public class SuppliersDto
    {
        public int SupplierId { get; set; }

       
        public int CustomerId { get; set; }

        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Country { get; set; }

     
        public string City { get; set; }

        public string Address { get; set; }



    }
}
