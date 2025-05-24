using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public static void DeleteTextChannelById(int channelId)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.TextChannels
                                .Include("Messages_S")
                                .FirstOrDefault(c => c.Id == channelId);

                if (channel != null)
                {
                    db.Messages_S.RemoveRange(channel.Messages_S);
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

        public static void UpdateRelaySettings(int channelId, string relayIp, int relayPort)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.TextChannels.FirstOrDefault(c => c.Id == channelId);
                if (channel != null)
                {
                    channel.RelayIp = relayIp;
                    channel.RelayPort = relayPort;
                    db.SaveChanges();
                }
            }
        }

        public static (string relayIp, int relayPort)? GetRelaySettings(int channelId)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.TextChannels.FirstOrDefault(c => c.Id == channelId);
                if (channel != null)
                {
                    return (channel.RelayIp, channel.RelayPort);
                }
                return null;
            }
        }

        public static List<Message_S> GetMessagesByChannelId(int channelId)
        {
            using (var db = new AppDbContext())
            {
                return db.Messages_S
                    .Where(m => m.TextChannelId == channelId)
                    .OrderBy(m => m.SendTime)
                    .ToList();
            }
        }
    }
}
