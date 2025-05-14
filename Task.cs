using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Task
    {
        public static int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public File TaskFile { get; set; }
        public Bitmap TaskPicture { get; set; }
        public List<TaskInfo> TaskInfo { get; set; }
        
        public Task() { }

        public Task(string name, string description, DateTime deadline, File taskFile, Bitmap taskPicture, List<TaskInfo> taskInfo)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
            TaskFile = taskFile;
            TaskPicture = taskPicture;
            TaskInfo = taskInfo;
        }

        public void UpdateTask(string name, string description, DateTime deadline, File taskFile, Bitmap taskPicture, List<TaskInfo> taskInfo)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
            TaskFile = taskFile;
            TaskPicture = taskPicture;
            TaskInfo = taskInfo;
        }
    }
}
