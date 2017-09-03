using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdieBook.Models;

namespace BirdieBook.ViewModels
{
    public class UserRoundViewModel
    {
        public UserRound UserRound { get; set; }
        public GolfCourse GolfCourse { get; set; }
        public TeeBox TeeBox { get; set; }

    }
}
