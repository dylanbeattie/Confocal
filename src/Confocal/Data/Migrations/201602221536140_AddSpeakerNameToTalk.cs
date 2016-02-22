namespace Confocal.Data.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddSpeakerNameToTalk : DbMigration {
        public override void Up() {
            AddColumn("dbo.Talk", "SpeakerName", c => c.String(maxLength: 128));
        }

        public override void Down() {
            DropColumn("dbo.Talk", "SpeakerName");
        }
    }
}
