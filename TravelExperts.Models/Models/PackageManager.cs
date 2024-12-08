using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Models
{
    public class PackageManager
    {
        public static List<Package> GetAllPackages(TravelExpertsContext db) {
            List<Package> packages = db.Packages.ToList();
            return packages;
        }

        public static Package getPackageById(TravelExpertsContext db,int? id)
        {
            return db.Packages.Find(id)!;
        }
        public static List<TripType> GetTripTypes(TravelExpertsContext db) { 
            return db.TripTypes.ToList();
        }
        public static List<Region> GetRegions(TravelExpertsContext db) {
            return db.Regions.ToList();
        }
        public static List<Class> GetClasses(TravelExpertsContext db) {
            return db.Classes.ToList();
        }
        public static List<Fee> GetFees(TravelExpertsContext db) { 
            return db.Fees.ToList();
        }

        public static List<ProductSupplierDTO> GetProductSuppliers(TravelExpertsContext db)
        {
            List<ProductSupplierDTO> productSuppliers = new List<ProductSupplierDTO>();
            productSuppliers = db.ProductsSuppliers.Join(db.Products,ps => ps.ProductId,p => p.ProductId,(ps,p) => new {ps,p})
                .Join(db.Suppliers,psp => psp.ps.SupplierId,s => s.SupplierId,(all,s) => new ProductSupplierDTO
                {
                    ProductSupplierId = all.ps.ProductSupplierId,
                    SupplierId = all.ps.SupplierId,
                    ProductId = all.ps.ProductId,
                    ProdName = all.p.ProdName,
                    SupName = s.SupName,
                }).ToList();
            return productSuppliers;
        }
    }
}
