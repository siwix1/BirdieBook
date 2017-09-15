using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirdieBook.Models
{
    public class GolfCourse
    {
        public GolfCourse()
        {
            TeeBox = new List<TeeBox>();
        }

        public string GolfCourseId { get; set; }

        [Display(Name = "Golf Course")]
        public string Name { get; set; }

        //public MapPosition Position  { get; set; }


        public virtual ICollection<TeeBox> TeeBox  { get; set; }
    }
}
