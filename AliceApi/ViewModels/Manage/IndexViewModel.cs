using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AliceApi.ViewModels.Manage
{
    public class IndexViewModel 
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public IList<IdentityRole> Roles { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public bool ProfileExists { get; set; }
        public Membership.MemberViewModel.MemberProfile MemberProfile { get; set; }

    }
}
