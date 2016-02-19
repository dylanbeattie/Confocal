namespace Confocal.Data.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTalkTable : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.Talk",
                c => new {
                    TalkId = c.Guid(nullable: false),
                    Title = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.TalkId);

        }

        public override void Down() {
            DropTable("dbo.Talk");
        }
    }
}
