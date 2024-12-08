﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.Models.Models
{
    public class BookingOrderModel
    {
        [Required]
        public double? TravelerCount { get; set; }
        public int? PackageId { get; set; }
        [Required]
        public string TripTypeId { get; set; } = string.Empty;
        [Required]
        public double? ItineraryNo { get; set; }
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Destination { get; set; } = string.Empty;
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }
        [Required]
        public string RegionId { get; set; } = string.Empty;
        [Required]
        public string ClassId { get; set; } = string.Empty;
        [Required]
        public string FeeId {  get; set; } = string.Empty;
        [Required]
        public int? ProductSupplierId { get; set; }
    }
}