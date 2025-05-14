using System;
using System.Collections.Generic;

namespace Collabry
{
    public class Workgroup
    {
        public int WorkgroupID { get; set; }
        public string WorkgroupName { get; set; }
        public List<WorkgroupGroup> WorkgroupGroups { get; set; }
        public List<Meeting> Meetings { get; set; }

        public Workgroup() { }

        public Workgroup(int workgroupID, string workgroupName)
        {
            WorkgroupID = workgroupID;
            WorkgroupName = workgroupName;
            WorkgroupGroups.Add(new WorkgroupGroup(WorkgroupGroups.Count+1, "management", "Direction", WorkgroupGroup.TypeGroup.Manager));
            WorkgroupGroups.Add(new WorkgroupGroup(WorkgroupGroups.Count+1, "class_a_2025", "1A Class", WorkgroupGroup.TypeGroup.Defualt));
            WorkgroupGroups.Add(new WorkgroupGroup(WorkgroupGroups.Count+1, "visitors", "Visitors", WorkgroupGroup.TypeGroup.None));
        }
    }
}