using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceApi.ViewModels.Movie
{
    public class InternetVideoArchiveViewModel
    {
        public InternetVideoArchiveViewModel()
        {
        }
        

        public class Image
        {
            public string URL { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Encode
        {
            public string URL { get; set; }
            public string Format { get; set; }
            public int BitRate { get; set; }
        }

        public class EmbedCode
        {
            public string Type { get; set; }
            public string EmbedHTML { get; set; }
        }

        public class Asset
        {
            public int PublishedId { get; set; }
            public string Title { get; set; }
            public bool isDefault { get; set; }
            public string MediaType { get; set; }
            public bool Mature { get; set; }
            public int OriginalVideoWidth { get; set; }
            public int OriginalVideoHeight { get; set; }
            public int DurationInSeconds { get; set; }
            public bool AllowAdvertising { get; set; }
            public string EncodeDate { get; set; }
            public string TargetAudience { get; set; }
            public List<Image> Images { get; set; }
            public object Captions { get; set; }
            public List<Encode> Encodes { get; set; }
            public List<EmbedCode> EmbedCodes { get; set; }
            public int ProprietaryCustomerId { get; set; }
        }

        public class RootObject
        {
            public string Title { get; set; }
            public int FirstReleasedYear { get; set; }
            public int PublishedId { get; set; }
            public string MediaType { get; set; }
            public List<string> Performers { get; set; }
            public string DateModified { get; set; }
            public List<string> NormalizedTitles { get; set; }
            public List<Asset> Assets { get; set; }
        }


    }
}
