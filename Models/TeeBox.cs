using System;
using System.Collections.Generic;
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
        
        public string TeeBoxID { get; set; }
        public string GolfCourseID { get; set; }
        public string Name { get; set; }
        public int MensSlope { get; set; } 
        public int MensCourseRating { get; set; }
        public int WomensSlope { get; set; }
        public int WomensCourseRating { get; set; }

        public virtual ICollection<Hole> holes { get; set; }

    }
}
