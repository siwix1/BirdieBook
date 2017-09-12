using BirdieBook.Models;
using System.Collections.Generic;

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
