using System;
using System.Linq;
using System.Web.Mvc;
using Confocal.Data;
using Confocal.Models;
using Microsoft.Ajax.Utilities;

namespace Confocal.Controllers {
    public class TalksController : Controller {
        public ActionResult Index() {
            using (var db = new ConfocalDbContext()) { return (View(db.Talks.ToList())); }
        }

        public ActionResult NotFound(string code) {
            return (View("NotFound", (object)code));
        }

        private Guid ReadUserGuid() {
            return (Request.RequestContext.HttpContext.Items["user-key"] is Guid ? (Guid)Request.RequestContext.HttpContext.Items["user-key"] : new Guid());
        }

        public ActionResult Feedback(string code) {
            if (String.IsNullOrEmpty(code)) return (RedirectToAction("Index", "Home"));
            var userGuid = ReadUserGuid();
            using (var db = new ConfocalDbContext()) {
                var talk = db.Talks.FirstOrDefault(t => t.Code == code);
                if (talk == default(Talk)) return (NotFound(code));
                var reaction = talk.Reactions.FirstOrDefault(fb => fb.UserGuid == userGuid);
                if (reaction == default(TalkReaction)) reaction = new TalkReaction() {
                    Talk = talk, TalkGuid = talk.TalkGuid, UserGuid = userGuid
                };
                var data = reaction.ToFeedbackViewData();
                return (View(data));
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Feedback(FeedbackViewData post) {
            var userGuid = ReadUserGuid();
            using (var db = new ConfocalDbContext()) {
                var reaction = db.TalkReactions.FirstOrDefault(t => t.UserGuid == userGuid && t.TalkGuid == post.TalkGuid);
                if (reaction == default(TalkReaction)) {
                    reaction = new TalkReaction {
                        UserGuid = userGuid,
                        TalkGuid = post.TalkGuid,
                        UserAgent = Request.UserAgent,
                        Submitted = DateTimeOffset.Now
                    };
                    db.TalkReactions.Add(reaction);
                }
                reaction.Enjoy = post.Enjoy;
                reaction.OneIdeaToImprove = post.OneIdeaToImprove;
                reaction.Learn = post.Learn;
                reaction.OneThingYouLiked = post.OneThingYouLiked;
                db.SaveChanges();
                return (RedirectToAction("Thankyou"));
            }
        }

        public ActionResult Thankyou() {
            return (View());
        }
    }

    public static class ModelExtensions {
        public static FeedbackViewData ToFeedbackViewData(this TalkReaction reaction) {
            return (new FeedbackViewData {
                TalkSubject = reaction.Talk.Title,
                SpeakerName = reaction.Talk.SpeakerName,
                TalkGuid = reaction.TalkGuid,
                Enjoy = reaction.Enjoy,
                Learn = reaction.Learn,
                OneThingYouLiked = reaction.OneThingYouLiked,
                OneIdeaToImprove = reaction.OneIdeaToImprove
            });
        }
    }
}