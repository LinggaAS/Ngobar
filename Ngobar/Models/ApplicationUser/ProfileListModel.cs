using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngobar.Models.ApplicationUser
{
    public class ProfileListModel
    {
        public IEnumerable<ProfileModel> Profiles { get; set; }
    }
}
