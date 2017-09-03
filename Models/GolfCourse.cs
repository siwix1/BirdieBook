using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class GolfCourse
    {
        public GolfCourse()
        {
            teeBox = new List<TeeBox>();
        }
        public string GolfCourseID { get; set; }

        [Display(Name = "Golf Course")]
        public string Name { get; set; }

        public virtual ICollection<TeeBox> teeBox  { get; set; }
    }
}
