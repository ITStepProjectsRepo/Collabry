using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class ServerRoleService
    {
        public static ServerRole AddRole(string roleName, string description = "", bool isAdmin = false,
            bool canManageMessages = false, bool canKickUsers = false, bool canBanUsers = false, bool canCreateChannels = false)
        {
            using (var db = new AppDbContext())
            {
                var role = new ServerRole
                {
                    RoleName = roleName,
                    Description = description,
                    IsAdmin = isAdmin,
                    CanManageMessages = canManageMessages,
                    CanKickUsers = canKickUsers,
                    CanBanUsers = canBanUsers,
                    CanCreateChannels = canCreateChannels
                };

                db.ServerRoles.Add(role);
                db.SaveChanges();
                return role;
            }
        }

        public static void AssignRoleToUser(int userId, int roleId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users_S.Include("ServerRoles").FirstOrDefault(u => u.Id == userId);
                var role = db.ServerRoles.Find(roleId);

                if (user != null && role != null && !user.ServerRoles.Contains(role))
                {
                    user.ServerRoles.Add(role);
                    db.SaveChanges();
                }
            }
        }

        public static void RemoveRoleFromUser(int userId, int roleId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users_S.Include("ServerRoles").FirstOrDefault(u => u.Id == userId);
                var role = db.ServerRoles.Find(roleId);

                if (user != null && role != null && user.ServerRoles.Contains(role))
                {
                    user.ServerRoles.Remove(role);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteRoleById(int roleId)
        {
            using (var db = new AppDbContext())
            {
                var role = db.ServerRoles.Include("Users_S").FirstOrDefault(r => r.Id == roleId);

                if (role != null)
                {
                    role.Users_S.Clear();
                    db.ServerRoles.Remove(role);
                    db.SaveChanges();
                }
            }
        }

        public static List<ServerRole> GetRolesByUserId(int userId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users_S.Include("ServerRoles").FirstOrDefault(u => u.Id == userId);
                return user?.ServerRoles.ToList() ?? new List<ServerRole>();
            }
        }

        public static List<ServerRole> GetAllRoles()
        {
            using (var db = new AppDbContext())
            {
                return db.ServerRoles.Include("Users_S").ToList();
            }
        }
    }
}
