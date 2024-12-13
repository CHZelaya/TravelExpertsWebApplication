using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.ViewModel
{
    public class EditProfileViewModel
    {
        // User Information        
        public double VirtualWallet { get; set; }// Default value

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName{ get; set; }

        [StringLength(1000, ErrorMessage = "Travel preference cannot exceed 1000 characters.")]
        public string? TravelPreference { get; set; }

        public byte[]? ProfilePicture { get; set; }

        // Customer Information
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(25, ErrorMessage = "First Name cannot exceed 25 characters.")]
        public string CustFirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(25, ErrorMessage = "Last Name cannot exceed 25 characters.")]
        public string CustLastName { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(75, ErrorMessage = "Address cannot exceed 75 characters.")]
        public string CustAddress { get; set; } = null!;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string CustCity { get; set; } = null!;

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, ErrorMessage = "Province must be exactly 2 characters.")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Province must only contain 2 uppercase alphabetic characters.")]
        public string CustProv { get; set; } = null!;

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(7, ErrorMessage = "Postal Code cannot exceed 7 characters.")]
        [RegularExpression("^[ABCEGHJ-NPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z][ -]?\\d[ABCEGHJ-NPRSTV-Z]\\d$",
        ErrorMessage = "Not a valid Canadian Postal Code.")]
        public string CustPostal { get; set; } = null!;


        [StringLength(25, ErrorMessage = "Country cannot exceed 25 characters.")]
        public string? CustCountry { get; set; }

        [Phone(ErrorMessage = "Invalid Home Phone format.")]
        [StringLength(10, ErrorMessage = "Home Phone cannot exceed 10 characters.")]
        public string? CustHomePhone { get; set; }

        [Required(ErrorMessage = "Business Phone is required.")]
        [Phone(ErrorMessage = "Invalid Business Phone format.")]
        [StringLength(10, ErrorMessage = "Business Phone cannot exceed 10 characters.")]
        public string CustBusPhone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        public string CustEmail { get; set; } = null!;
    }
}
