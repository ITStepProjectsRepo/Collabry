using System;
using System.Collections.Generic;

namespace Collabry
{
    public enum NotificationSetting
    {
        None,
        OnlyMention,
        AllMessages
    }
    public class UserPM
    {
        

        public string OtherUserTag {get; set;}
        public NotificationSetting NotificationSetting {get; set;}
        public List<Message> Messages {get; set;}

        public UserPM()
        {
            OtherUserTag = string.Empty;
            this.NotificationSetting = NotificationSetting.AllMessages;
            Messages = new List<Message>();
        }
    }
}