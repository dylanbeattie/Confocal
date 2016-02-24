using System.Collections.Generic;

namespace Confocal.Models {
    public class TalkListViewData {
        public IEnumerable<TalkViewData> AllTalks { get; set; }
        public TalkViewData CurrentTalk { get; set; }
    }
}