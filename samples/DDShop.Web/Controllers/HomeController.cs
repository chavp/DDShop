﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDShop.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}