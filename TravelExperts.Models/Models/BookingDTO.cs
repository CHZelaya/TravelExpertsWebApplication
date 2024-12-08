using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Models
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; } = string.Empty;
        public double? TravelerCount { get; set; }
        public int? CustomerId { get; set; }
        public string TripTypeId { get; set; } = string.Empty;
        public string TTName { get; set; } = string.Empty;
        public int? PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public decimal? PackagePrice { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
