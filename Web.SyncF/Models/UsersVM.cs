using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Web.SyncF.Models
{
    public class UsersVM : IdentityUser
    {
        public string Password { get; set; }
        public string Route { get; set; }
    }
}
