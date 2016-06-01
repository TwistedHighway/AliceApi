using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Results;
using AliceApi.Models;
using Microsoft.Ajax.Utilities;
using AliceApi.Common;

namespace AliceApi.ViewModels.Membership
{
    public class MemberViewModel
    {

        public MemberViewModel()
        {
            this.Genders = new List<string>() { "-- Choose One --", "Male", "Female" };
        }

        public IEnumerable<Repository.Enums.MemberType> MemberTypes { get; set; }

        public IEnumerable<string> Genders { get; set; }

        public IList<Member> Members { get; set; }
        

        public class MemberProfile : Member
        {
              public Guid MemberProfileId { get; set; }          
            public string AspNetUserId { get; set; }// AspNetUserId
            
            public string LocalUserName { get; set; }

            [Display(Name="Public View User Name")]
            public string PublicUserName { get; set; }


            [Display(Name="Primary Email")]
            public string PrimaryEmail { get; set; }


            [Display(Name="List of Members")]
            public IList<Member> Members { get; set; }

            public Member PrimaryMember { get; set; }


            [Display(Name="Date Updated")]
            public DateTime? DateUpdated { get; set; }

            
            public string ErrorCode { get; set; }
            public string ResponseMessage { get; set; }
        }

        public class Member
        {
            public Member()
            {
                this.DateOfBirth = null;
            }

            public Guid MemberId { get; set; }



            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            public int MemberType { get; set; }


            public string MemberEmail { get; set; }


        [Required(ErrorMessage = "Please enter a Gender")]
            [Display(Name = "Gender")]
            [RegularExpression("^(Male|Female)$", ErrorMessage = "Please choose a valid Gender")]
            public string Gender { get; set; }

            [Required(ErrorMessage = "Please enter a Date Of Birth")]
            [Display(Name = "Date Of Birth")]
            [BirthDateValidation(ErrorMessage = "Date Of Birth must be in the past")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            [DataType(DataType.Date)]
            public DateTime? DateOfBirth { get; set; }

        }

        //[Required(ErrorMessage = "Please enter a Effective Date")]
        //[Display(Name = "Effective Date")]
        //[EffectiveDateValidation(ErrorMessage = "Effective Date must be in the future")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[DataType(DataType.Date)]
        //public DateTime? EffectiveDate { get; set; }



    }
}