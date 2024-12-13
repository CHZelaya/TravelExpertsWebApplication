using Microsoft.AspNetCore.Identity;

namespace TravelExpertsData.Models
{
    public class User : IdentityUser
    {
        public double VirtualWallet { get; set; } = 30000.00;//default value
        public string? TravelPreference { get; set; }
        public byte[]? ProfilePicture { get; set; }//can be nullable
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}