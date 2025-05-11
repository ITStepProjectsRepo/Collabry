using System;
using System.Collections.Generic;

namespace Collabry
{
    public enum NotificationSettings
    {
        None,
        OnlyMention,
        AllMessages
    }
    public class UserPM
    {
        

        public string OtherUserTag {get; set;}
        public NotificationSettings NotificationSettings {get; set;}
        public List<Message> Messages {get; set;}

        public UserPM()
        {
            OtherUserTag = string.Empty;
            this.NotificationSettings = NotificationSettings.AllMessages;
            Messages = new List<Message>();
        }
    }
}