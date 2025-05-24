using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Collabry
{
    public class User_S
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int ServerId { get; set; }

        [ForeignKey(nameof(ServerId))]
        public virtual Server Server { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.Now;

        public bool IsMuted { get; set; } = false;
        public bool IsBanned { get; set; } = false;

        [NotMapped]
        public VoiceChannelClient VoiceChannelClient { get; set; }

        public virtual List<ServerRole> ServerRoles { get; set; } = new List<ServerRole>();
    }

    public class UserIntroPacket
    {
        public int Id { get; set; }
        public string UserTag { get; set; }
        public string UserName { get; set; }
        public bool IsMuted { get; set; }
        public bool IsBanned { get; set; }
        public DateTime JoinedAt { get; set; }
        public byte[] UserPictureData { get; set; }

        public string DisplayName => $"{UserName} {(IsMuted ? "[Микрофон выкл]" : "[Микрофон вкл]")}";

        public byte[] ToBytes()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            {
                writer.Write(Id);
                writer.Write(UserTag ?? "");
                writer.Write(UserName ?? "");
                writer.Write(IsMuted);
                writer.Write(IsBanned);
                writer.Write(JoinedAt.ToBinary());

                writer.Write(UserPictureData?.Length ?? 0);
                if (UserPictureData != null)
                    writer.Write(UserPictureData);

                return ms.ToArray();
            }
        }

        public static UserIntroPacket FromBytes(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var reader = new BinaryReader(ms))
            {
                var packet = new UserIntroPacket
                {
                    Id = reader.ReadInt32(),
                    UserTag = reader.ReadString(),
                    UserName = reader.ReadString(),
                    IsMuted = reader.ReadBoolean(),
                    IsBanned = reader.ReadBoolean(),
                    JoinedAt = DateTime.FromBinary(reader.ReadInt64())
                };

                int imageLength = reader.ReadInt32();
                if (imageLength > 0)
                    packet.UserPictureData = reader.ReadBytes(imageLength);

                return packet;
            }
        }
    }
}
