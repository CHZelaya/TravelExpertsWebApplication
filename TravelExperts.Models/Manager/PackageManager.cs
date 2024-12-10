using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

namespace TravelExpertsData.Manager
{
    public class PackageManager
    {
        public static List<Package> GetAllPackages(TravelExpertsContext db)
        {
            List<Package> packages = db.Packages.ToList();
            return packages;
        }

        public static Package getPackageById(TravelExpertsContext db, int? id)
        {
            return db.Packages.Find(id)!;
        }
        public static List<TripType> GetTripTypes(TravelExpertsContext db)
        {
            return db.TripTypes.ToList();
        }
        public static List<Region> GetRegions(TravelExpertsContext db)
        {
            return db.Regions.ToList();
        }
        public static List<Class> GetClasses(TravelExpertsContext db)
        {
            return db.Classes.ToList();
        }
        public static List<Fee> GetFees(TravelExpertsContext db)
        {
            return db.Fees.ToList();
        }

        public static List<ProductSupplierDTO> GetProductSuppliersByPackageId(TravelExpertsContext db, int? PackageId)
        {
            List<ProductSupplierDTO> productSuppliers = new List<ProductSupplierDTO>();

            productSuppliers = db.PackagesProductsSuppliers.Join(db.ProductsSuppliers, pps => pps.ProductSupplierId, ps => ps.ProductSupplierId, (pps, ps) => new { pps, ps })
                .Join(db.Products, ppsps => ppsps.ps.ProductId, p => p.ProductId, (ppsps, p) => new { ppsps, p })
                .Join(db.Suppliers, ppspsp => ppspsp.ppsps.ps.SupplierId, s => s.SupplierId, (all, s) => new ProductSupplierDTO
                {
                    ProductSupplierId = all.ppsps.pps.ProductSupplierId,
                    PackageId = all.ppsps.pps.PackageId,
                    SupplierId = all.ppsps.ps.SupplierId,
                    ProductId = all.ppsps.ps.ProductId,
                    ProdName = all.p.ProdName,
                    SupName = s.SupName,
                }).Where(query => query.PackageId == PackageId).ToList();
            return productSuppliers;
        }
    }
}
