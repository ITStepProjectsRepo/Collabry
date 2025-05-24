using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class User_SService
    {
        public static User_S AddUserToServer(int userId, int serverId)
        {
            using (var db = new AppDbContext())
            {
                var existing = db.Users_S.FirstOrDefault(u => u.UserId == userId && u.ServerId == serverId);
                if (existing != null) return existing;

                var user_S = new User_S
                {
                    UserId = userId,
                    ServerId = serverId,
                    JoinedAt = DateTime.Now
                };

                db.Users_S.Add(user_S);
                db.SaveChanges();
                return user_S;
            }
        }
    }
}
