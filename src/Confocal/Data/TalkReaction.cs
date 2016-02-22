using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Confocal.Data {
    public class TalkReaction {
        [Key, Column(Order = 0)]
        public Guid UserGuid { get; set; }
        [Key, Column(Order = 1), ForeignKey("Talk")]
        public Guid TalkGuid { get; set; }

        public virtual Talk Talk { get; set; }
        [StringLength(3)]
        public string Enjoy { get; set; }
        [StringLength(3)]
        public string Learn { get; set; }
        [StringLength(256)]
        public string OneThingYouLiked { get; set; }
        [StringLength(256)]
        public string OneIdeaToImprove { get; set; }

        public DateTimeOffset? Submitted { get; set; }
        public string UserAgent { get; set; }
    }
}