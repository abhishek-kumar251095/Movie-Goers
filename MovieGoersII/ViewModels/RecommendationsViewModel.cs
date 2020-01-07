using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.ViewModels
{
    public class RecommendationsViewModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int Intensity { get; set; }
        public int CharacterCentered { get; set; }
        public int Violence { get; set; }
        public int InformationRequired { get; set; }
        public int Pace { get; set; }
        public int SpecialEffects { get; set; }
        public int Suspense { get; set; }
            
        public List<SelectListItem> MoviesList { get; set; }
        
    }
}
