using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieGoersIIBL
{
    public class UserCollection
    {

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public bool IsSoftDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movies Movie { get; set; }
    }
}
