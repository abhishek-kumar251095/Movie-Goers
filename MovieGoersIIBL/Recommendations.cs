using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieGoersIIBL
{
    public class Recommendations
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        [Range(1, 5, ErrorMessage ="Ratings must be between 1 to 5")]
        public int Intensity { get; set; }
        [Range(1, 5, ErrorMessage = "Ratings must be between 1 to 5")]
        public int CharacterCentered { get; set; }
        [Range(1, 5, ErrorMessage = "Ratings must be between 1 to 5")]
        public int Violence { get; set; }
        [Range(1, 5, ErrorMessage = "Ratings must be between 1 to 5")]
        public int InformationRequired { get; set; }
        [Range(1, 5, ErrorMessage = "Ratings must be between 1 to 5")]
        public int Pace { get; set; }
        [Range(1, 5, ErrorMessage = "Ratings must be between 1 to 5")]
        public int SpecialEffects { get; set; }
        [Range(1, 5, ErrorMessage = "Ratings must be between 1 to 5")]
        public int Suspense { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movies Movie { get; set; }
    }
}
