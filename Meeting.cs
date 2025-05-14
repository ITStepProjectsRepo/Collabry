using System;
using System.Collections.Generic;

namespace Collabry
{
    public class Meeting
    {
        public int Id { get; set; }
        public string MeetingName { get; set; }
        public DateTime MeetingTime { get; set; }
        public TimeSpan MeetingDuration { get; set; }
        public List<string> InvitedTags { get; set; }

        public Meeting() { }

        public Meeting(int id, string meetingName, DateTime meetingTime, TimeSpan meetingDuration, List<string> invitedTags)
        {
            Id = id;
            MeetingName = meetingName;
            MeetingTime = meetingTime;
            MeetingDuration = meetingDuration;
            InvitedTags = invitedTags;
        }
    }
}