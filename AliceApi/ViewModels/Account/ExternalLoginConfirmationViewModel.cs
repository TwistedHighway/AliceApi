using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AliceApi.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }

        //public string PublicViewName { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address1 { get; set; }
    }
}
