using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Confocal.Data {
    public class Talk {
        [Key]
        public Guid TalkGuid { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(128)]
        public string SpeakerName { get; set; }

        [StringLength(6)]
        public string Code { get; set; }

        public virtual ICollection<TalkReaction> Reactions { get; set; }
    }
}