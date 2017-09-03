using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class GolfCourseDetails
    {
        public GolfCourse GolfCourse { get; set; }
        public IEnumerable<TeeBox> TeeBox { get; set; }
        public IEnumerable<Hole> Hole { get; set; }
    }
}
