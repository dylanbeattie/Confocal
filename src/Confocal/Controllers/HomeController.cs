using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Confocal.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }
    }
}