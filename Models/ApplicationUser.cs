using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FavCourses = new List<GolfCourse>();
        }


        public IEnumerable<GolfCourse> FavCourses { get; set; } //Favourite courses

        public DateTime? BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public enum GenderType
        {
            Male=0,
            Female=1
        }

        public decimal Hcp { get; set; } //Handicap

        //public string Country { get; set; } //Homeplace

    }

}
