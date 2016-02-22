using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Confocal.Data.Migrations;

namespace Confocal.Data {
    public class ConfocalDbContext : DbContext {
        public ConfocalDbContext() {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ConfocalDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<TalkReaction>().HasRequired(p => p.Talk).WithMany(t => t.Reactions);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Talk> Talks { get; set; }
        public DbSet<TalkReaction> TalkReactions { get; set; }
    }
}