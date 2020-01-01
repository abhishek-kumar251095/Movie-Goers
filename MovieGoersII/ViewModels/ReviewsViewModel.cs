using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.ViewModels
{
    public class ReviewsViewModel
    {
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
    }
}
