namespace Confocal.Data.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class SetDefaultTalkIdToNewSequentialId : DbMigration {
        public override void Up() {
            AlterColumn("dbo.Talk", "TalkId", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
        }

        public override void Down() {
            AlterColumn("dbo.Talk", "TalkId", c => c.Guid(nullable: false, identity: true, defaultValueSql: ""));
        }
    }
}
