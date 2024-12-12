using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(1000, ErrorMessage = "Travel preference cannot exceed 1000 characters.")]
        public string? TravelPreference { get; set; }//optional

        public CustomerViewModel Customer { get; set; }
    }

    public class CustomerViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }        

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, ErrorMessage = "Province must be exactly 2 characters.")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Province must only contain 2 uppercase alphabetic characters.")]
        public string CustProv { get; set; }

        [Required(ErrorMessage = "Business phone is required.")]
        [Phone(ErrorMessage = "Enter a valid phone number.")]
        public string CustBusPhone { get; set; }

        [Required]
        public string CustAddress { get; set; }
    }
}
