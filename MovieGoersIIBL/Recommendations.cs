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
        public int Intensity { get; set; }
        public int CharacterCentered { get; set; }
        public int Violence { get; set; }
        public int InformationRequired { get; set; }
        public int Pace { get; set; }
        public int SpecialEffects { get; set; }
        public int Suspense { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movies Movie { get; set; }
    }
}
