using BirdieBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.ViewModels
{
    public class UserRoundCreateViewModel
    {
        public UserRound UserRound { get; set; }
        public List<GolfCourse> GolfCourses { get; set; }
        public List<TeeBox> TeeBoxes { get; set; }
        public List<UserScore> UserScores { get; set; }
    }
}
