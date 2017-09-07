using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class TeeBox
    {
        public TeeBox()
        {
            holes = new List<Hole>();
        }
        [Key]
        [HiddenInput(DisplayValue =false)]
        public string TeeBoxID { get; set; }

        [Display(Name="Golf Course")]
        [HiddenInput(DisplayValue = false)]
        [ForeignKey("GolfCourse")]
        public string GolfCourseID { get; set; }

        [Display(Name="Tee")]
        public string Name { get; set; }
        public int MensSlope { get; set; } 
        public int MensCourseRating { get; set; }
        public int WomensSlope { get; set; }
        public int WomensCourseRating { get; set; }

        public virtual ICollection<Hole> holes { get; set; }

    }
}
