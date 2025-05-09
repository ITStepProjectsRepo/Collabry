using System;

namespace Collabry
{
    public class Workgroup
    {
        public int Id { get; set; }
        public string WorkgroupName { get; set; }

        public List<WorkgroupGroup>  WorkgroupGroups { get; set; }
    }
}