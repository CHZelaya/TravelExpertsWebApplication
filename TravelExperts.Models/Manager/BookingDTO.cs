using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Manager
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BookingDate { get; set; }
        [DisplayName("Booking Number")]
        public string BookingNo { get; set; } = string.Empty;
        [DisplayName("Traveler Count")]
        public double? TravelerCount { get; set; }
        public int? CustomerId { get; set; }
        public string TripTypeId { get; set; } = string.Empty;
        [DisplayName("Trip Type")]
        public string TTName { get; set; } = string.Empty;
        public int? PackageId { get; set; }
        [DisplayName("Package Name")]
        public string PackageName { get; set; } = string.Empty;
        [DisplayName("Package Price")]
        public decimal? PackagePrice { get; set; }
        [DisplayName("Total Cost")]
        public decimal? TotalCost { get; set; }
    }
}
