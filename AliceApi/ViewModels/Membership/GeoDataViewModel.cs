using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.ViewModels.Membership
{
    public class GeoDataViewModel
    {

        [Required(ErrorMessage = "Please enter a Zip Code")]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Please enter a valid format Zip Code")]
        public int ZipCode { get; set; }


    }
}
