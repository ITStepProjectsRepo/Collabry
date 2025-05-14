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
        public DbSet<User> Users { get; set; }
        public DbSet<User_S> Users_S { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerRole> ServerRoles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Message_S> Messages_S { get; set; }
        public DbSet<TextChannel> TextChannels { get; set; }

        public AppDbContext()
            : base("name=CollabryDb")
        {
        }

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
