using System.Collections.Generic;
using System.Linq;

namespace Collabry
{
    public class ServerService
    {
        public static Server CreateServer(string name)
        {
            using (var db = new AppDbContext())
            {
                var server = new Server
                {
                    Name = name
                };

                db.Servers.Add(server);
                db.SaveChanges();
                return server;
            }
        }

        public static void DeleteServerById(int serverId)
        {
            using (var db = new AppDbContext())
            {
                var server = db.Servers
                    .Include("ServerMembers")
                    .Include("ServerRoles")
                    .Include("ServerChannels")
                    .Include("ServerGroups")
                    .FirstOrDefault(s => s.Id == serverId);

                if (server != null)
                {
                    server.ServerMembers.Clear();
                    server.ServerRoles.Clear();
                    server.ServerChannels.Clear();
                    server.ServerGroups.Clear();

                    db.Servers.Remove(server);
                    db.SaveChanges();
                }
            }
        }

        public static User_S AddUserToServer(int serverId, int userId)
        {
            using (var db = new AppDbContext())
            {
                var server = db.Servers.Include("ServerMembers").FirstOrDefault(s => s.Id == serverId);
                var user = db.Users.Find(userId);

                if (server == null || user == null)
                    return null;

                bool exists = db.Users_S.Any(us => us.ServerId == serverId && us.UserId == userId);
                if (exists)
                    return db.Users_S.First(us => us.ServerId == serverId && us.UserId == userId);

                var user_s = new User_S { UserId = user.Id, ServerId = server.Id };
                db.Users_S.Add(user_s);
                db.SaveChanges();

                return user_s;
            }
        }

        public static void RemoveUserFromServer(int serverId, int userId)
        {
            using (var db = new AppDbContext())
            {
                var server = db.Servers.Include("ServerMembers").FirstOrDefault(s => s.Id == serverId);
                var user = db.Users_S.Find(userId);

                if (server != null && user != null && server.ServerMembers.Contains(user))
                {
                    server.ServerMembers.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public static List<Server> GetAllServers()
        {
            using (var db = new AppDbContext())
            {
                return db.Servers
                    .Include("ServerMembers")
                    .Include("ServerRoles")
                    .Include("ServerChannels")
                    .Include("ServerGroups")
                    .ToList();
            }
        }

        public static Server GetServerById(int serverId)
        {
            using (var db = new AppDbContext())
            {
                return db.Servers
                    .Include("ServerMembers")
                    .Include("ServerRoles")
                    .Include("ServerChannels")
                    .Include("ServerGroups")
                    .FirstOrDefault(s => s.Id == serverId);
            }
        }
    }
}
