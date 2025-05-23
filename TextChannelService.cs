using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class TextChannelService
    {
        public static TextChannel CreateTextChannel(int serverId, string name)
        {
            using (var db = new AppDbContext())
            {
                var server = db.Servers.Find(serverId);
                if (server == null)
                    return null;

                var channel = new TextChannel
                {
                    ServerId = serverId,
                    Name = name,
                    Messages_S = new List<Message_S>()
                };

                db.TextChannels.Add(channel);
                db.SaveChanges();
                return channel;
            }
        }

        public static void DeleteTextChannel(int channelId)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.TextChannels.Include("Messages_S").FirstOrDefault(c => c.Id == channelId);
                if (channel != null)
                {
                    db.TextChannels.Remove(channel);
                    db.SaveChanges();
                }
            }
        }

        public static TextChannel GetTextChannelById(int channelId)
        {
            using (var db = new AppDbContext())
            {
                return db.TextChannels
                         .Include("Messages_S")
                         .FirstOrDefault(c => c.Id == channelId);
            }
        }

        public static List<TextChannel> GetTextChannelsByServerId(int serverId)
        {
            using (var db = new AppDbContext())
            {
                return db.TextChannels
                         .Where(c => c.ServerId == serverId)
                         .Include("Messages_S")
                         .ToList();
            }
        }

        public static void RenameTextChannel(int channelId, string newName)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.TextChannels.Find(channelId);
                if (channel != null)
                {
                    channel.Name = newName;
                    db.SaveChanges();
                }
            }
        }
    }
}
