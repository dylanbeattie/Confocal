using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConjaWeb.Models {
    public class FeedbackViewData {

        public string TalkSubject { get; set; }
        public string SpeakerName { get; set; }

        public int SessionId { get; set; }
        public Guid TalkGuid { get; set; }
        public string Enjoy { get; set; }
        public string Learn { get; set; }
        public string OneThingYouLiked { get; set; }
        public string OneIdeaToImprove { get; set; }

        public string SubmitterName { get; set; }
        public string SubmitterInfo { get; set; }

        public class Option {
            public string Name { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
            public string Label { get; set; }
        }

        private Option MakeOption(string value, string label, string currentValue) {
            return (new Option {
                Value = value,
                Label = label,
                Selected = currentValue == value
            });
        }


        public IEnumerable<Option> EnjoyOptions {
            get {
                yield return (MakeOption("zzz", "I was falling asleep...", this.Enjoy));
                yield return (MakeOption("meh", "It was OK, I guess", this.Enjoy));
                yield return (MakeOption("yes", "I liked it.", this.Enjoy));
                yield return (MakeOption("wow", "I loved it", this.Enjoy));
            }
        }

        public IEnumerable<Option> LearnOptions {
            get {
                yield return (MakeOption("err", "I found it confusing.", this.Learn));
                yield return (MakeOption("meh", "It was OK, I guess", this.Learn));
                yield return (MakeOption("yes", "I liked it.", this.Learn));
                yield return (MakeOption("wow", "I loved it", this.Learn));
            }
        }
    }
}