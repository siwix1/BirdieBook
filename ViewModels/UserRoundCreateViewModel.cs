using System;
using BirdieBook.Models;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdieBook.ViewModels
{
    public class UserRoundCreateViewModel
    {
        public UserRoundCreateViewModel()
        {

            var hcpList = new List<SelectListItem>();
            for (decimal i = -540; i < -360; i += 10)
            {
                hcpList.Add(
                    new SelectListItem
                    {
                        Text = Math.Abs(i / 10).ToString(CultureInfo.InvariantCulture),
                        Value = (i / 10).ToString(CultureInfo.InvariantCulture)
                    }
                );
            }
            for (decimal i = -360; i <= 0; i++)
            {
                hcpList.Add(
                    new SelectListItem
                    {
                        Text = Math.Abs(i / 10).ToString("0.0" ),
                        Value = (i).ToString(CultureInfo.InvariantCulture)
                    }
                );
            }
            for (decimal i = 1 ; i <= 80; i++)
            {
                hcpList.Add(
                    new SelectListItem
                    {
                        Text = "+" + (i / 10).ToString("0.0"),
                        Value = (i).ToString((CultureInfo.InvariantCulture))
                    }
                );
            }

            HcpList = hcpList;
        }

        public List<SelectListItem> HcpList { get; set; }

    }
}
