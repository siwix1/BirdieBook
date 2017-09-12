using System;

namespace BirdieBook.ViewModels
{
    public class UserRoundViewModel
    {
        public string UserRoundId { get; set; }
        public string GolfCourse { get; set; }
        public string Tee { get; set; }
        public DateTime TeeTime { get; set; }
        public int TotalScore { get; set; } //Sum Number of shots taken per hole
        public int HolesPlayed { get; set; } //Number of holes played
    }
}
