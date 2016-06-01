using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace AliceApi.Models
{
    public class BirthDateValidation : ValidationAttribute
    {
        public BirthDateValidation() { }

        public override bool IsValid(object value)
        {
            // Address a nullable type. 
            // Remove this if it's a required field
            if (value == null)
                return true;

            var dt = (DateTime)value;
            if (dt < DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
    public class EffectiveDateValidation : ValidationAttribute
    {
        public EffectiveDateValidation() { }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }

}