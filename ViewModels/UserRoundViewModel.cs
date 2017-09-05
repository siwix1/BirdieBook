using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdieBook.Models;

namespace BirdieBook.ViewModels
{
    public class UserRoundViewModel
    {
        public string UserRoundID { get; set; }
        public string GolfCourse { get; set; }
        public string Tee { get; set; }
        public DateTime TeeTime { get; set; }
        public int TotalScore { get; set; } //Sum Number of shots taken per hole
        public int HolesPlayed { get; set; } //Number of holes played
    }
}
