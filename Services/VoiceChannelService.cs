using System.Linq;

namespace Collabry
{
    public class VoiceChannelService
    {
        public static VoiceChannel CreateVoiceChannel(int serverId, string name)
        {
            using (var db = new AppDbContext())
            {
                var server = db.Servers.Find(serverId);
                if (server == null)
                    return null;

                var channel = new VoiceChannel
                {
                    ServerId = serverId,
                    Name = name
                };

                db.VoiceChannels.Add(channel);
                db.SaveChanges();
                return channel;
            }
        }

        public static void DeleteVoiceChannelById(int channelId)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.VoiceChannels.FirstOrDefault(c => c.Id == channelId);
                if (channel != null)
                {
                    db.VoiceChannels.Remove(channel);
                    db.SaveChanges();
                }
            }
        }

        public static VoiceChannel GetVoiceChannelById(int vcId)
        {
            using (var db = new AppDbContext())
            {
                return db.VoiceChannels.FirstOrDefault(vc => vc.Id == vcId);
            }
        }

        public static void UpdateRelaySettings(int channelId, string relayIp, int relayPort)
        {
            using (var db = new AppDbContext())
            {
                var channel = db.VoiceChannels.FirstOrDefault(c => c.Id == channelId);
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
                var channel = db.VoiceChannels.FirstOrDefault(c => c.Id == channelId);
                if (channel != null)
                {
                    return (channel.RelayIp, channel.RelayPort);
                }
                return null;
            }
        }
    }
}
