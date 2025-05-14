using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ServerName = name
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

        public static void AddUserToServer(int serverId, int userId)
        {
            using (var db = new AppDbContext())
            {
                var server = db.Servers.Include("ServerMembers").FirstOrDefault(s => s.Id == serverId);
                var user = db.Users_S.Find(userId);

                if (server != null && user != null && !server.ServerMembers.Contains(user))
                {
                    server.ServerMembers.Add(user);
                    db.SaveChanges();
                }
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
