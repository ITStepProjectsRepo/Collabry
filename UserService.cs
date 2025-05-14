using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class UserService
    {
        public static User AddUser(string userTag, string userName, string email, string password, string userInfo = "", byte[] pictureData = null)
        {
            using (var db = new AppDbContext())
            {
                var user = new User
                {
                    UserTag = userTag,
                    UserName = userName,
                    Email = email,
                    Password = password,
                    UserInfo = userInfo,
                    UserPictureData = pictureData
                };

                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
        }

        public static void DeleteUser(int userId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }

        public static User GetUserById(int userId)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.Find(userId);
            }
        }

        public static List<User> GetAllUsers()
        {
            using (var db = new AppDbContext())
            {
                return db.Users.ToList();
            }
        }

        public static void UpdateUserById(int userId, string newUserName = null, string newEmail = null, string newPassword = null, string newInfo = null, byte[] newPicture = null)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    if (newUserName != null) user.UserName = newUserName;
                    if (newEmail != null) user.Email = newEmail;
                    if (newPassword != null) user.Password = newPassword;
                    if (newInfo != null) user.UserInfo = newInfo;
                    if (newPicture != null) user.UserPictureData = newPicture;

                    db.SaveChanges();
                }
            }
        }
    }
}
