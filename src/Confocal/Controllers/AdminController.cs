using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Confocal.Data;
using Confocal.Models;

namespace Confocal.Controllers {
    public class AdminController : Controller {
        public ViewResult Login() {
            return View();
        }

        private string ConfigUsername { get { return (ConfigurationManager.AppSettings["admin.username"]); } }
        private string ConfigPassword { get { return (ConfigurationManager.AppSettings["admin.password"]); } }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (model.UserName == ConfigUsername && model.Password == ConfigPassword) {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View();
        }

        [Authorize]
        public ViewResult Index() {
            using (var db = new ConfocalDbContext()) {
                var model = new TalkListViewData();
                model.AllTalks = db.Talks.Select(t => new TalkViewData() {
                    Title = t.Title, SpeakerName = t.SpeakerName, Code = t.Code, TalkGuid = t.TalkGuid
                }).ToList();
                return View(model);
            }
        }

        public ActionResult CreateTalk(TalkViewData post) {
            using (var db = new ConfocalDbContext()) {
                var talk = new Talk() {
                    TalkGuid = Guid.NewGuid(),
                    Code = post.Code,
                    Title = post.Title,
                    SpeakerName = post.SpeakerName,
                };
                db.Talks.Add(talk);
                db.SaveChanges();
                return (RedirectToAction("Index"));
            }
        }

        public ActionResult Reactions(Guid talk) {
            using (var db = new ConfocalDbContext()) {
                var talkFromDb = db.Talks.FirstOrDefault(t => t.TalkGuid == talk);
                if (talkFromDb == default(Talk)) return (RedirectToAction("Index"));
                return (View(talkFromDb.Reactions.ToList()));
            }
        }
    }

    public class TalkListViewData {
        public IEnumerable<TalkViewData> AllTalks { get; set; }
        public TalkViewData CurrentTalk { get; set; }
    }
}