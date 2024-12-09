using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData.Models;

public partial class Package
{
    [Key]
    public int PackageId { get; set; }

    [StringLength(50)]
    [DisplayName("Package Name")]
    public string PkgName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    [DisplayName("Start Date")]
    public DateTime? PkgStartDate { get; set; }

    [Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    [DisplayName("End Date")]
    public DateTime? PkgEndDate { get; set; }

    [StringLength(50)]
    [DisplayName("Description")]
    public string? PkgDesc { get; set; }

    [Column(TypeName = "money")]
    [DisplayName("Price")]
    public decimal PkgBasePrice { get; set; }

    [Column(TypeName = "money")]
    [DisplayName("Administrative Charges")]
    public decimal? PkgAgencyCommission { get; set; }

    [InverseProperty("Package")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Package")]
    public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; } = new List<PackagesProductsSupplier>();
}
