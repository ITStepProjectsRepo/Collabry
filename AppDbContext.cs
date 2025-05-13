using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=CollabryDb")
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<TextChannel> TextChannels { get; set; }

        static AppDbContext()
        {
            // Database initialization
            // Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
