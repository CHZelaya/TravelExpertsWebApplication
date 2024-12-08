using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.Models.Models
{
    public class BookingManager
    {
        public static List<BookingDTO> getBooksbyCustomerId(TravelExpertsContext db,int CustomerId = 135)
        {
            List<BookingDTO> bookings = new List<BookingDTO>();
            bookings = db.Bookings.Join(db.Packages, b => b.PackageId,p => p.PackageId,(b,p) => new {b,p}).
                Join(db.TripTypes,bp => bp.b.TripTypeId,t => t.TripTypeId,(bp,t) => 
                new BookingDTO
                {
                    BookingId = bp.b.BookingId,
                    BookingDate = bp.b.BookingDate,
                    BookingNo = bp.b.BookingNo!,
                    TravelerCount = bp.b.TravelerCount,
                    CustomerId = bp.b.CustomerId,
                    TripTypeId = bp.b.TripTypeId!,
                    TTName = t.Ttname!,
                    PackageId = bp.b.PackageId,
                    PackageName = bp.p.PkgName,
                    PackagePrice = db.BookingDetails.Where(bd => bd.BookingId == bp.b.BookingId)
                    .Sum(bd => (decimal?)bd.BasePrice + bd.AgencyCommission) ?? 0,
                    TotalCost = ((decimal?)bp.b.TravelerCount ?? 0) * (db.BookingDetails.Where(bd => bd.BookingId == bp.b.BookingId)
                    .Sum(bd => (decimal?)bd.BasePrice + bd.AgencyCommission) ?? 0)
                }).Where(booking => booking.CustomerId == CustomerId)
                .ToList();
            return bookings;
        }

        public static List<BookingDetailDTO> GetBookingDetails(TravelExpertsContext db,int BookingId)
        {
            List<BookingDetailDTO> bookingDetails = new List<BookingDetailDTO>();
            bookingDetails = db.BookingDetails.Join(db.Regions, bd => bd.RegionId, r => r.RegionId, (bd, r) => new { bd, r })
                .Join(db.Classes, bdr => bdr.bd.ClassId, c => c.ClassId, (bdr, c) => new { bdr, c })
                .Join(db.Fees, bdrc => bdrc.bdr.bd.FeeId, f => f.FeeId, (bdrc, f) => new { bdrc, f }).
                Join(db.ProductsSuppliers, bdrcf => bdrcf.bdrc.bdr.bd.ProductSupplierId, ps => ps.ProductSupplierId, (bdrcf, ps) => new { bdrcf, ps })
                .Join(db.Products, bdrcfPS => bdrcfPS.ps.ProductId, p => p.ProductId, (bdrcfPS, p) => new { bdrcfPS, p })
                .Join(db.Suppliers, all => all.bdrcfPS.ps.SupplierId, s => s.SupplierId, (all, s) => new BookingDetailDTO
                {
                    BookingDetailId = all.bdrcfPS.bdrcf.bdrc.bdr.bd.BookingDetailId,
                    ItineraryNo = all.bdrcfPS.bdrcf.bdrc.bdr.bd.ItineraryNo,
                    TripStart = all.bdrcfPS.bdrcf.bdrc.bdr.bd.TripStart,
                    TripEnd = all.bdrcfPS.bdrcf.bdrc.bdr.bd.TripEnd,
                    Description = all.bdrcfPS.bdrcf.bdrc.bdr.bd.Description!,
                    Destination = all.bdrcfPS.bdrcf.bdrc.bdr.bd.Destination!,
                    BasePrice = all.bdrcfPS.bdrcf.bdrc.bdr.bd.BasePrice,
                    AgencyCommission = all.bdrcfPS.bdrcf.bdrc.bdr.bd.AgencyCommission,
                    BookingId = all.bdrcfPS.bdrcf.bdrc.bdr.bd.BookingId,
                    RegionId = all.bdrcfPS.bdrcf.bdrc.bdr.bd.RegionId!,
                    RegionName = all.bdrcfPS.bdrcf.bdrc.bdr.r.RegionName!,
                    ClassId = all.bdrcfPS.bdrcf.bdrc.bdr.bd.ClassId!,
                    ClassName = all.bdrcfPS.bdrcf.bdrc.c.ClassName!,
                    FeeId = all.bdrcfPS.bdrcf.bdrc.bdr.bd.FeeId!,
                    FeeName = all.bdrcfPS.bdrcf.f.FeeName!,
                    ProductSupplierId = all.bdrcfPS.ps.ProductSupplierId,
                    ProductName = all.p.ProdName!,
                    SupplierName = s.SupName!,
                }).Where(detail => detail.BookingId == BookingId).ToList();
            return bookingDetails;
        }
        public static List<Booking> GetBookings(TravelExpertsContext db)
        {
            List<Booking> bookings = db.Bookings.ToList();
            return bookings;
        }
        public static int CreateBooking(TravelExpertsContext db,Booking booking)
        {
            int bookingId = 0;
            try
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                bookingId = booking.BookingId;
            }
            catch (Exception ex) {
                throw ex;
            }
            return bookingId;
        }

        public static bool CreateBookingDetail(TravelExpertsContext db,BookingDetail bookingDetail)
        {
            bool result = false;
            try
            {
                db.BookingDetails.Add(bookingDetail);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static bool CheckBalance(TravelExpertsContext db,int UserId,double totalCost)
        {
            bool result = false;
            var User = db.Users.Find(UserId);
            if (User != null) { 
                if(User.VirtualWallet > totalCost)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool CheckItinerary(TravelExpertsContext db,double? ItineraryNo)
        {
            bool result = false;
            result = db.BookingDetails.Any(bd => bd.ItineraryNo == ItineraryNo);
            return result;
        }
    }
}
