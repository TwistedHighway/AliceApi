using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AliceApi.ViewModels
{
        [MetadataType(typeof(Movie_Validation))]
    public class MovieViewModel 
    {

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }





        // this technique is not the easiest one to implement.  
        private string _firstname;
        private string _lastname;
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string FirstName 
        {
            get { return _firstname; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("FirstName", "FirstName is required");

                _firstname = value;
            }
        }

        public string LastName
        {
            get { return _lastname; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("LastName", "LastName is required");

                _lastname = value;
            }
        }


        public string this[string columnName]
        {
            get 
            {
                if (_errors.ContainsKey(columnName))
                    return _errors[columnName];

                return String.Empty;
            }

        
        }

        public string Error
        {
            get{ return string.Empty;}
        }

        public class Movie_Validation
        {
            [Required(ErrorMessage = "Title is required")]
            public string MovieTitle { get; set; }

            //[Required(ErrorMessage = "Address is Required")]
            //public string Address1 { get; set; }

            [DataType(DataType.Date)]// seems redundant but it describes a DataType of Date 
            [DisplayFormat(DataFormatString = "{0:d}")]
            //[Required(ErrorMessage = " is Required")]// adding this invalidates the update/insert
            public DateTime DateUpdated { get; set; }

        }


    }
}