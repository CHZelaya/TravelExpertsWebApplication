using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

namespace TravelExpertsData.Manager
{
    public class BookingManager
    {
        public static List<BookingDTO> getBooksbyCustomerId(TravelExpertsContext db, int CustomerId = 135)
        {
            List<BookingDTO> bookings = new List<BookingDTO>();
            bookings = db.Bookings.Join(db.Packages, b => b.PackageId, p => p.PackageId, (b, p) => new { b, p }).
                Join(db.TripTypes, bp => bp.b.TripTypeId, t => t.TripTypeId, (bp, t) =>
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
                    .Sum(bd => bd.BasePrice + bd.AgencyCommission) ?? 0,
                    TotalCost = ((decimal?)bp.b.TravelerCount ?? 0) * (db.BookingDetails.Where(bd => bd.BookingId == bp.b.BookingId)
                    .Sum(bd => bd.BasePrice + bd.AgencyCommission) ?? 0)
                }).Where(booking => booking.CustomerId == CustomerId)
                .ToList();
            return bookings;
        }

        public static List<BookingDetailDTO> GetBookingDetails(TravelExpertsContext db, int BookingId)
        {
            List<BookingDetailDTO> bookingDetails = new List<BookingDetailDTO>();
            bookingDetails = db.BookingDetails
                .Join(db.ProductsSuppliers, bd => bd.ProductSupplierId, ps => ps.ProductSupplierId, (bd, ps) => new { bd, ps })
                .Join(db.Products, bdps => bdps.ps.ProductId, p => p.ProductId, (bdps, p) => new { bdps, p })
                .Join(db.Suppliers, bdpsp => bdpsp.bdps.ps.SupplierId, s => s.SupplierId, (all, s) => new BookingDetailDTO
                {
                    BookingDetailId = all.bdps.bd.BookingDetailId,
                    ItineraryNo = all.bdps.bd.ItineraryNo,
                    TripStart = all.bdps.bd.TripStart,
                    TripEnd = all.bdps.bd.TripEnd,
                    Description = all.bdps.bd.Description!,
                    Destination = all.bdps.bd.Destination!,
                    BasePrice = all.bdps.bd.BasePrice,
                    AgencyCommission = all.bdps.bd.AgencyCommission,
                    BookingId = all.bdps.bd.BookingId,
                    ProductSupplierId = all.bdps.ps.ProductSupplierId,
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
        public static int CreateBooking(TravelExpertsContext db, Booking booking)
        {
            int bookingId = 0;
            try
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                bookingId = booking.BookingId;
            }
            catch (Exception ex)
            {
                throw;
            }
            return bookingId;
        }

        public static bool CreateBookingDetail(TravelExpertsContext db, BookingDetail bookingDetail)
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
                throw;
            }
            return result;
        }

        public static bool CheckBalance(TravelExpertsContext db, int UserId, double totalCost)
        {
            bool result = false;
            var User = db.Users.Find(UserId);
            if (User != null)
            {
                if (User.VirtualWallet > totalCost)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool CheckItinerary(TravelExpertsContext db, double? ItineraryNo)
        {
            bool result = false;
            result = db.BookingDetails.Any(bd => bd.ItineraryNo == ItineraryNo);
            return result;
        }
    }
}
