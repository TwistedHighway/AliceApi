using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AliceApi.Repository.Models;

namespace AliceApi.ViewModels.Movie
{


    [MetadataType(typeof(Movie_Validation))]
    public class MovieViewModel
    {

        //public MovieViewModel()
        //{

        //}
        public class ResultSetJson
        {
            public ResultSet ResultSet { get; set; }
        }
        public class ResultSet
        {
            [DataMember]
            public bool isSuccess { get; set; }
            [DataMember]
            public string failedReason { get; set; }
            [DataMember]
            public System.Net.HttpStatusCode statusCode { get; set; }
            [DataMember]
            public string Description { get; set; }
        }
        public IList<ChuckNorrisFacts> FactsAboutChuckNorris { get; set; }

        public class ChuckNorrisFacts
        {
            public string FactId { get; set; }
            public string Fact { get; set; }
            public string FactCategories { get; set; }
        }
        //http://json2csharp.com/#
        public class Value
        {
            public int id { get; set; }
            public string joke { get; set; }
            public List<object> categories { get; set; }
        }

        public class RootObject
        {
            public string type { get; set; }
            public List<Value> value { get; set; }
        }



        //public Movie Movie { get; set; }
        public class Movie
        {
            
            public int MovieId { get; set; }
            public int Genre { get; set; }
            public string MovieTitle { get; set; }

            //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
            [DataType(DataType.Date)]
            public DateTime? ReleaseDate { get; set; }
            public string Summary { get; set; }
            public int MpaaRatingId { get; set; }

            public string MpaaRated { get; set; }
            public int LocalRating { get; set; }
            public string DirectedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateUpdated { get; set; }
            public string UpdatedBy { get; set; }
            
            public IList<Movie> Movies { get; set; }


            public string GenreList { get; set; }

            public IList<MovieGenre> Genres { get; set; }

            public int ErrorId { get; set; }
            public string Message { get; set; }

            // public Repository.Models.Movie MovieData { get; set; }
        }

        //[RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Please enter a valid format Zip Code")]


        //private string _firstname;
        //private string _lastname;
        //private Dictionary<string, string> _errors = new Dictionary<string, string>();


        //[Required(ErrorMessage = "Please Enter First Name")]
        //[Display(Name = "First Name")]
        //public string FirstName 
        //{
        //    get { return _firstname; }
        //    set
        //    {
        //        if (String.IsNullOrEmpty(value))
        //            _errors.Add("FirstName", "FirstName is required");

        //        _firstname = value;
        //    }
        //}

        //public string LastName
        //{
        //    get { return _lastname; }
        //    set
        //    {
        //        if (String.IsNullOrEmpty(value))
        //            _errors.Add("LastName", "LastName is required");

        //        _lastname = value;
        //    }
        //}


        //public string this[string columnName]
        //{
        //    get 
        //    {
        //        if (_errors.ContainsKey(columnName))
        //            return _errors[columnName];

        //        return String.Empty;
        //    }


        //}

        public string Error { get; set; }

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