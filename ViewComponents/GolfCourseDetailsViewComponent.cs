using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdieBook.Models;


namespace BirdieBook.ViewComponents
{
    public class GolfCourseDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(GolfCourseDetails golfCourseDetails)
        {

            return View(golfCourseDetails);

        }
    }
}
