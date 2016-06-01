using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AliceApi.ViewModels.Movie
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Genre { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Summary { get; set; }
        public int MpaaRatingId { get; set; }
        public int LocalRating { get; set; }
        public string DirectedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public Repository.Models.Movie MovieData { get; set; }
    }
}