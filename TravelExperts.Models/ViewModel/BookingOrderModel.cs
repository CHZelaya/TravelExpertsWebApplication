using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.ViewModel
{
    public class BookingOrderModel
    {
        [Required]
        public double? TravelerCount { get; set; }
        public int? PackageId { get; set; }
        [Required]
        [Display(Name = "Trip Type")]
        public string TripTypeId { get; set; } = string.Empty;
        public double? ItineraryNo { get; set; }
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Destination { get; set; } = string.Empty;
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }
        //[Required]
        //[Display(Name = "Region")]
        //public string RegionId { get; set; } = string.Empty;
        //[Required]
        //[Display(Name = "Class")]
        //public string ClassId { get; set; } = string.Empty;
        //[Required]
        //[Display(Name = "Fee")]
        //public string FeeId {  get; set; } = string.Empty;
        //[Required]
        [Display(Name = "Vendor")]
        public int? ProductSupplierId { get; set; }
    }
}
