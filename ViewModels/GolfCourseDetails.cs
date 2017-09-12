using System.Collections.Generic;
using BirdieBook.Models;

namespace BirdieBook.ViewModels
{
    public class GolfCourseDetails
    {
        public GolfCourse GolfCourse { get; set; }
        public IEnumerable<TeeBox> TeeBox { get; set; }
        public IEnumerable<Hole> Hole { get; set; }
    }
}
