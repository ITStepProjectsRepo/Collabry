using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Collabry
{
    public class User
    {
        public string UserTag { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public BitmapImage UserPicture { get; set; }
        public string UserInfo { get; set; }
        public Dictionary<string, object[]> ChatList { get; set;}

        public User() { }
    }
}
