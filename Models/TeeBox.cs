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
        [Display(Name = "Mens Slope")]
        public int? MensSlope { get; set; }
        [Display(Name = "Mens Course Rating")]
        public int? MensCourseRating { get; set; }
        [Display(Name = "Womens Slope")]
        public int? WomensSlope { get; set; }
        [Display(Name = "Womens Course Rating")]
        public int? WomensCourseRating { get; set; }
        [Display(Name = "Unit Of Measure")]
        public DistanceType UnitOfMeasure { get; set; }

        public enum DistanceType
        {
            Meters=0,
            Yards=1
        }

        public virtual ICollection<Hole> holes { get; set; }

    }
}
