using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Manager
{
    public class BookingDetailDTO
    {
        public int BookingDetailId { get; set; }
        [DisplayName("Itinerary Number")]
        public double? ItineraryNo { get; set; }
        [DisplayName("Trip Start")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TripStart { get; set; }
        [DisplayName("Trip End")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }
        [DisplayName("Total Price")]
        public decimal? TotalPrice => BasePrice + AgencyCommission;
        public int? BookingId { get; set; }
        //public string RegionId { get; set; } =  string.Empty ;
        //public string RegionName { get; set; } = string.Empty;
        //public string ClassId { get; set; } = string.Empty;
        //public string ClassName { get; set; } = string.Empty;
        //public string FeeId {  get; set; } = string.Empty;
        //public string FeeName { get; set;} = string.Empty;
        public int? ProductSupplierId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;

    }
}
