using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliceApi.ViewModels
{
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




    }
}