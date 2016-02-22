namespace Confocal.Data.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateTalkReaction : DbMigration {
        public override void Up() {
            DropPrimaryKey("dbo.Talk");
            CreateTable(
                "dbo.TalkReaction",
                c => new {
                    UserGuid = c.Guid(nullable: false),
                    TalkGuid = c.Guid(nullable: false),
                    Enjoy = c.String(maxLength: 3),
                    Learn = c.String(maxLength: 3),
                    OneThingYouLiked = c.String(maxLength: 256),
                    OneIdeaToImprove = c.String(maxLength: 256),
                })
                .PrimaryKey(t => new { t.UserGuid, t.TalkGuid })
                .ForeignKey("dbo.Talk", t => t.TalkGuid, cascadeDelete: true)
                .Index(t => t.TalkGuid);
            RenameColumn("dbo.Talk", "TalkId", "TalkGuid");
            AddPrimaryKey("dbo.Talk", "TalkGuid");

            AddColumn("dbo.Talk", "Code", c => c.String(maxLength: 6));
        }

        public override void Down() {
            DropForeignKey("dbo.TalkReaction", "TalkGuid", "dbo.Talk");
            DropIndex("dbo.TalkReaction", new[] { "TalkGuid" });
            DropTable("dbo.TalkReaction");

            DropColumn("dbo.Talk", "Code");
            DropPrimaryKey("dbo.Talk");
            RenameColumn("dbo.Talk", "TalkGuid", "TalkId");
            AddPrimaryKey("dbo.Talk", "TalkId");
        }
    }
}
