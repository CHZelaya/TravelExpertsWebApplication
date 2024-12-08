using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using TravelExpertsData.Models;

namespace TravelExpertsMVC.Controllers
{
    public class VacationController : Controller
    {
        TravelExpertsContext _context;
        public VacationController(TravelExpertsContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            List<Package> packages = PackageManager.GetAllPackages(_context);
            return View(packages);
        }

        public IActionResult Order(int id)
        {
            getSelectItems();
            Package package = PackageManager.getPackageById(_context, id);
            ViewBag.Package = package;
            return View();
        }

        [HttpPost]
        public IActionResult Order(BookingOrderModel model)
        {
            getSelectItems();
            Package package = PackageManager.getPackageById(_context, model.PackageId);
            ViewBag.Package = package;
            if (ModelState.IsValid)
            {
                int balance = 500;
                var TotalCount = (decimal)model.TravelerCount! * (model.BasePrice + model.AgencyCommission);
                try
                {
                    if (balance < TotalCount)
                    {
                        throw new Exception("Insufficient fund,you don not have enough money to place this order!");
                    }
                    if (BookingManager.CheckItinerary(_context, model.ItineraryNo))
                    {
                        throw new Exception("This ItineraryNo already exist!");
                    }
                    Booking booking = new Booking { 
                        BookingDate = DateTime.Now,
                        BookingNo = getUniqueString(),
                        TravelerCount = model.TravelerCount,
                        CustomerId = 135,
                        TripTypeId = model.TripTypeId,
                        PackageId = model.PackageId,
                    };
                    var bookingId = BookingManager.CreateBooking(_context,booking);
                    BookingDetail bookingDetail = new BookingDetail
                    {
                        ItineraryNo = model.ItineraryNo,
                        TripStart = model.TripStart,
                        TripEnd = model.TripEnd,
                        Description = model.Description,
                        Destination = model.Destination,
                        BasePrice = model.BasePrice,
                        AgencyCommission = model.AgencyCommission,
                        BookingId = bookingId,
                        RegionId = model.RegionId,
                        ClassId = model.ClassId,
                        FeeId = model.FeeId,
                        ProductSupplierId = model.ProductSupplierId,
                    };
                    var result = BookingManager.CreateBookingDetail(_context, bookingDetail);
                    if(result)
                    {
                        return RedirectToAction("Index","Booking");
                    } else
                    {
                        TempData["errorMessage"] = "Something is Wrong while you placing new order.";
                    }
                }
                catch (Exception ex) {
                    TempData["errorMessage"] = ex.Message;
                    throw;
                }
            }
            return View(model);
        }
        private void getSelectItems()
        {
            List<TripType> tripTypeList = PackageManager.GetTripTypes(_context);
            var TripTypeList = new SelectList(tripTypeList, "TripTypeId", "Ttname").ToList();
            ViewBag.TripTypeList = TripTypeList;
            List<Region> regionList = PackageManager.GetRegions(_context);
            var RegionList = new SelectList(regionList, "RegionId", "RegionName").ToList();
            ViewBag.RegionList = RegionList;
            List<Class> classList = PackageManager.GetClasses(_context);
            var ClassList = new SelectList(classList, "ClassId", "ClassName").ToList();
            ViewBag.ClassList = ClassList;
            List<Fee> feeList = PackageManager.GetFees(_context);
            var FeeList = new SelectList(feeList, "FeeId", "FeeName").ToList();
            ViewBag.FeeList = FeeList;
            List<ProductSupplierDTO> productSuppliers = PackageManager.GetProductSuppliers(_context);
            var ProductSupplierList = new SelectList(productSuppliers, "ProductSupplierId", "displayName").ToList();
            ViewBag.ProductSupplierList = ProductSupplierList;
        }

        private string getUniqueString()
        {
            return "BK" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}
