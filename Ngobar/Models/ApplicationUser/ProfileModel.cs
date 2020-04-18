using Microsoft.AspNetCore.Http;
using System;

namespace Ngobar.Models.ApplicationUser
{
    public class ProfileModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserRating { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime MemberSejak { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
