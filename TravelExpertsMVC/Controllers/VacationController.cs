using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using TravelExpertsData.Manager;
using TravelExpertsData.Models;
using TravelExpertsData.ViewModel;

namespace TravelExpertsMVC.Controllers
{
    public class VacationController : Controller
    {
        private readonly UserManager<User> _userManager;

        TravelExpertsContext _context;
        public VacationController(TravelExpertsContext context,UserManager<User> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<Package> packages = PackageManager.GetAllPackages(_context);
            return View(packages);
        }

        public IActionResult Order(int id)
        {
            getSelectItems(id);
            Package package = PackageManager.getPackageById(_context, id);
            ViewBag.Package = package;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order(BookingOrderModel model)
        {
            getSelectItems(model.PackageId);
            Package package = PackageManager.getPackageById(_context, model.PackageId);
            ViewBag.Package = package;
            var user = await _userManager.GetUserAsync(User) as User;
            if (ModelState.IsValid)
            {
                var TotalCount = (decimal)model.TravelerCount! * (model.BasePrice + model.AgencyCommission);
                try
                {
                    if ((decimal)user!.VirtualWallet < TotalCount)
                    {
                        throw new Exception("Insufficient fund,you don not have enough money to place this order!");
                    }
                    user.VirtualWallet = user.VirtualWallet - (double)TotalCount!;
                    var userUpdate = await _userManager.UpdateAsync(user);
                    if (!userUpdate.Succeeded)
                    {
                        throw new Exception("Transaction failed,Please try it later.");
                    }
                    Booking booking = new Booking { 
                        BookingDate = DateTime.Now,
                        BookingNo = getUniqueString(),
                        TravelerCount = model.TravelerCount,
                        CustomerId = user.CustomerId,
                        TripTypeId = model.TripTypeId,
                        PackageId = model.PackageId,
                    };
                    var bookingId = BookingManager.CreateBooking(_context,booking);
                    BookingDetail bookingDetail = new BookingDetail
                    {
                        ItineraryNo = getRandomNumber(),
                        TripStart = model.TripStart,
                        TripEnd = model.TripEnd,
                        Description = model.Description,
                        Destination = model.Destination,
                        BasePrice = model.BasePrice,
                        AgencyCommission = model.AgencyCommission,
                        BookingId = bookingId,
                        //RegionId = model.RegionId,
                        //ClassId = model.ClassId,
                        //FeeId = model.FeeId,
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
                }
            }
            return View(model);
        }
        private void getSelectItems(int? PackageId)
        {
            List<TripType> tripTypeList = PackageManager.GetTripTypes(_context);
            var TripTypeList = new SelectList(tripTypeList, "TripTypeId", "Ttname").ToList();
            ViewBag.TripTypeList = TripTypeList;
            //List<Region> regionList = PackageManager.GetRegions(_context);
            //var RegionList = new SelectList(regionList, "RegionId", "RegionName").ToList();
            //ViewBag.RegionList = RegionList;
            //List<Class> classList = PackageManager.GetClasses(_context);
            //var ClassList = new SelectList(classList, "ClassId", "ClassName").ToList();
            //ViewBag.ClassList = ClassList;
            //List<Fee> feeList = PackageManager.GetFees(_context);
            //var FeeList = new SelectList(feeList, "FeeId", "FeeName").ToList();
            //ViewBag.FeeList = FeeList;
            List<ProductSupplierDTO> productSuppliers = PackageManager.GetProductSuppliersByPackageId(_context,PackageId);
            var ProductSupplierList = new SelectList(productSuppliers, "ProductSupplierId", "displayName").ToList();
            ViewBag.ProductSupplierList = ProductSupplierList;
        }

        private string getUniqueString()
        {
            return "BK" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        private double getRandomNumber()
        {
            Random random = new Random();
            double randomNumber = random.Next(0, 10000);
            return Math.Round(randomNumber, 5);
        }
    }
}
