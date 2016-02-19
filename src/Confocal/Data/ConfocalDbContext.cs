using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Talk> Talks { get; set; }
    }

    public class Talk {
        public Guid TalkId { get; set; }
        [StringLength(128)]
        public string Title { get; set; }
    }
}