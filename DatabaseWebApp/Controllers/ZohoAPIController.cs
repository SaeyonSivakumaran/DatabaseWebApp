﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseWebApp.Models;
using System.Threading.Tasks;

namespace DatabaseWebApp.Controllers
{
    public class ZohoAPIController : Controller
    {
        public async Task<ActionResult> Index(string code)
        {
            ApiHelper.InitializeClient();
            await this.LoadWeather();
            if (!string.IsNullOrEmpty(code))
            {
                ViewData["displayCode"] = true;
                ViewData["code"] = code;
            }
            else
            {
                ViewData["displayCode"] = false;
            }
            return View();
        }

        private async Task LoadWeather()
        {
            WeatherModel weather = await WeatherProcessor.LoadWeather();
            ViewData["temperature"] = weather.Temp;
            ViewData["icon"] = $"http://openweathermap.org/img/w/{ weather.Icon }.png";
            ViewData["description"] = weather.Description;
            ViewData["city"] = weather.City;
            ViewData["country"] = weather.Country;
        }

        public ActionResult OAuthRedirect()
        {
            string clientId = "1000.6HOB8P0UQQ093190608HV63TWPQ6AH";
            string scope = "ZohoProjects.projects.READ";
            string redirect = "";
            string redirectUrl = $"https://accounts.zoho.com/oauth/v2/auth?scope={ scope }&client_id={ clientId }&response_type=code&access_type=offline&redirect_uri={ redirect }/&prompt=consent";
            return Redirect(redirectUrl);
        }
    }
}