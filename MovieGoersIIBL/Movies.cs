using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieGoersIIBL
{
    public class Movies
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public int TMDBId { get; set; }
        public string IMDBId { get; set; }
        public string Overview { get; set; }
        public string Language { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Status { get; set; }
        public int? Runtime { get; set; }
        public bool IsAdminRated { get; set; }

        public virtual ICollection<UserCollection> UserCollection { get; set; }
        public virtual ICollection<Recommendations> Recommendations{ get; set; }
    }
        
}
