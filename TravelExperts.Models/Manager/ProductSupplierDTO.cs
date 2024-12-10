using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Manager
{
    public class ProductSupplierDTO
    {
        public int ProductSupplierId { get; set; }
        public int? ProductId { get; set; }
        public int? SupplierId { get; set; }
        public int? PackageId { get; set; }
        public string? ProdName { get; set; }
        public string? SupName { get; set; }
        public string? displayName => $"Prod:{ProdName}  ,  Supplier:{SupName}";
    }
}
