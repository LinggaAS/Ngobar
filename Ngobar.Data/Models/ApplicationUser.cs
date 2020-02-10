using Microsoft.AspNetCore.Identity;
using System;

namespace Ngobar.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSejak { get; set; }
        public bool Status { get; set; }
    }
}
