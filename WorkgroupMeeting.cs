using System;

namespace Collabry
{
    public class WorkgroupMeeting
    {
        public int Id { get; set; }
        public string MeetingName { get; set; }
        public DateTime MeetingTime { get; set; }
        public TimeSpan MeetingDuration { get; set; }
    }
}