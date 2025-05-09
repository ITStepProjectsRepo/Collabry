using System;

namespace Collabry
{
    public class UserPM
    {
        public enum NotificationSettings
        {
            None,
            OnlyMention,
            AllMessages
        }

        public string OtherUserTag {get; set;}
        public NotificationSettings NotificationSettings {get; set;}
        public List<Message> Messages {get; set;}

        public UserPM()
        {
            OtherUserTag = string.Empty;
            NotificationSettings = NotificationSettings.AllMessages;
            Messages = new List<Message>();
        }

               

    }
}