using System;
using System.Collections.Generic;

namespace Collabry
{
    public class WorkgroupGroup
    {
        public enum TypeGroup
        {
            None = -1,
            Defualt,
            Operator,
            Manager
        }
        public int GroupID { get; set; }
        public string WorkgroupGroupTag { get; set; }
        public string WorkgroupGroupName { get; set; }
        public TypeGroup GroupType { get; set; }
        public List<Message> WorkgroupGroupChat { get; set; }
        public List<User> Users { get; set; }
        public List<Task> GroupTasks { get; set; }

        public WorkgroupGroup() { }

        public WorkgroupGroup(int groupID, string workgroupGroupTag, string workgroupGroupName, TypeGroup groupType)
        {
            GroupID = groupID;
            WorkgroupGroupTag = workgroupGroupTag;
            WorkgroupGroupName = workgroupGroupName;
            GroupType = groupType;
        }

        public void ChangeTag(string newTag)
        {
            WorkgroupGroupTag = newTag;
        }
        public void ChangeName(string newName)
        {
            WorkgroupGroupName = newName;
        }
        public void ChangeType(TypeGroup newType)
        {
            GroupType = newType;
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }
        public void AddTask(Task task)
        {
            GroupTasks.Add(task);
        }
        public void DeleteTask(Task task)
        {
            GroupTasks.Remove(task);
        }
        public void UpdateTask(int ID, Task task)
        {
            GroupTasks[ID] = task;
        }
    }
}