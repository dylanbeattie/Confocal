using System;
using System.Linq;
using System.Web.Mvc;
using Confocal.Data;
using Confocal.Models;

namespace Confocal.Controllers {
    public class TalksController : Controller {
        public ActionResult Index() {
            using (var db = new ConfocalDbContext()) { return (View(db.Talks.ToList())); }
        }

        public ActionResult NotFound(string code) {
            return (View("NotFound", (object)code));
        }

        public ActionResult Feedback(string code) {
            if (String.IsNullOrEmpty(code)) return (RedirectToAction("Index", "Home"));
            Guid userGuid;
            TalkReaction reaction;
            Guid.TryParse(User.Identity.Name, out userGuid);
            using (var db = new ConfocalDbContext()) {
                var talk = db.Talks.FirstOrDefault(t => t.Code == code);
                if (talk == default(Talk)) return (NotFound(code));
                reaction = talk.Reactions.FirstOrDefault(fb => fb.UserGuid == userGuid);
                if (reaction == default(TalkReaction)) reaction = new TalkReaction() {
                    Talk = talk, TalkGuid = talk.TalkGuid, UserGuid = userGuid
                };
                var data = reaction.ToFeedbackViewData();
                return (View(data));
            }
        }

        [HttpPost]
        public ActionResult Feedback(FeedbackViewData post) {
            Guid userGuid;
            TalkReaction reaction;
            Guid.TryParse(User.Identity.Name, out userGuid);
            using (var db = new ConfocalDbContext()) {
                reaction = db.TalkReactions.FirstOrDefault(t => t.UserGuid == userGuid && t.TalkGuid == post.TalkGuid);
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