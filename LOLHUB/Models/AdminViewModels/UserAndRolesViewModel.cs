using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LOLHUB.Models.AdminViewModels
{
    public class UserAndRolesViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string Rolename { get; set; }
    }
}
