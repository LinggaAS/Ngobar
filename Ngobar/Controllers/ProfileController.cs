using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ngobar.Data;
using Ngobar.Data.Models;
using Ngobar.Models.ApplicationUser;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ngobar.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            IApplicationUser userService,
            IUpload uploadService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSejak = user.MemberSejak,
                IsAdmin = userRoles.Contains("Admin")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            // konek ke azure storage account container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            // Get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString);

            // Parse the content disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            // ambil nama file
            var filename = contentDisposition.FileName.Trim('"');

            // Get a reference to a block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            // di block blop, upload file <-- file telah diupload
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            // set the user profile image to the URI
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            // redirect ke page user profile
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
    }
}