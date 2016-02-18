using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConjaWeb.Models;

namespace ConjaWeb.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }
    }

    public class TalksController : Controller {
        public ActionResult Feedback(string code) {
            var data = new FeedbackViewData();
            return (View(data));
        }
    }


}