using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public static class MessageService
    {
        public static void AddMessage(string text, string sender, int channelId, File file = null)
        {
            using (var db = new AppDbContext())
            {
                var message = new Message
                {
                    Text = text,
                    Sender = sender,
                    TextChannelId = channelId,
                    SendTime = DateTime.Now,
                    File = file,
                    IsEdited = false
                };

                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public static List<Message> GetMessagesByChannel(int channelId)
        {
            using (var db = new AppDbContext())
            {
                return db.Messages
                    .Include("Sender")
                    .Where(m => m.TextChannelId == channelId)
                    .OrderBy(m => m.SendTime)
                    .ToList();
            }
        }
    }

}
