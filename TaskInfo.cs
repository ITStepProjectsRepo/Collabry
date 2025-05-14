using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class TaskInfo
    {
        public enum TaskStatus
        {
            Default = -1,
            Assigned,
            OnCheck,
            Marked
        }
        public static int TaskInfoID { get; set; }
        public string UserTag { get; set; }
        public TaskStatus Status { get; set; }
        public int TaskMark { get; set; }
        public File UserAnswer { get; set; }
        public string UserCommentaries { get; set; }

        public override string ToString()
        {
            return $"ID: {TaskInfoID}\n" +
                $"User: {UserTag}\n" +
                $"Task Status: {Status}\n" +
                $"Mark: {TaskMark}\n" +
                $"File: {UserAnswer.FileData.Length} bytes\n" +
                $"Commentaries: {UserCommentaries}\n" +
                $"-=====================================================-";
        }
        public TaskInfo() { }

        public TaskInfo(string userTag, TaskStatus status, int taskMark, File userAnswer, string userCommentaries)
        {
            UserTag = userTag;
            Status = status;
            TaskMark = taskMark;
            UserAnswer = userAnswer;
            UserCommentaries = userCommentaries;
        }

        public void UpdateTaskInfo(string userTag, TaskStatus status, int taskMark, File userAnswer, string userCommentaries)
        {
            UserTag = userTag;
            Status = status;
            TaskMark = taskMark;
            UserAnswer = userAnswer;
            UserCommentaries = userCommentaries;
        }

        public void DeleteFile()
        {
            if (Status != TaskStatus.Marked)
            {
                UserAnswer = new File();
            }
        }
        public void UpdateFile()
        {
            if (Status != TaskStatus.Marked)
            {
                UserAnswer = new File();
                UserCommentaries = "";
            }
        }
    }
}
