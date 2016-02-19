using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Confocal.Models;

namespace Confocal.Controllers {
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

        [HttpPost]
        public ActionResult Feedback(FeedbackViewData post) {
            return (View(post));
        }
    }
}