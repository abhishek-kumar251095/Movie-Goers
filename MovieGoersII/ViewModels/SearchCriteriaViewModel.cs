using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.ViewModels
{
    public class SearchCriteriaViewModel
    {
        public string Page { get; set; }
        public string SearchQuery { get; set; }
        public List<SearchViewModel> SearchModel { get; set; }
    }
}
