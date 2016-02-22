namespace Confocal.Data.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTalkReactionMetadata : DbMigration {
        public override void Up() {
            AddColumn("dbo.TalkReaction", "Submitted", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.TalkReaction", "UserAgent", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.TalkReaction", "UserAgent");
            DropColumn("dbo.TalkReaction", "Submitted");
        }
    }
}
