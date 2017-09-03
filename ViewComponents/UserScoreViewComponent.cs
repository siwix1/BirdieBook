using BirdieBook.Models;
using BirdieBook.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.ViewComponents
{
    public class UserScoreViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserRoundCreateViewModel userRoundCreate) //TODO:Change model
        {
            

            return View(userRoundCreate);

        }
    }
}
