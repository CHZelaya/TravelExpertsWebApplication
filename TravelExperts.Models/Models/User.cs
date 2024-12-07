using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Models
{
    public class User : IdentityUser
    {
        public double VirtualWallet { get; set; }
        public string TravelPreference { get; set; }
        public byte[] ProfilePicture { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}