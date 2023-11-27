using Diary.Models.Base;
using System.Text;

namespace Diary.Models
{
    public class TaskList(int id, string title, List<string> tasks, DateTime creationDate) : Record(id, title, creationDate)
    {
        public List<string> Tasks { get; set; } = tasks;

        public override string DisplayInfo()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Task List - {Title}");
            sb.AppendLine($"Number of Tasks: {Tasks.Count}");
            sb.AppendLine($"Last Task: {(Tasks.Count > 0 ? Tasks.Last() : "No tasks")}");
            sb.AppendLine($"Created on: {CreationDate:yyyy-MM-dd HH:mm:ss}");
            return sb.ToString();
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Task List - {Title}");
            sb.AppendLine("Tasks:");
            foreach (var task in Tasks)
            {
                sb.AppendLine($"- {task}");
            }
            sb.AppendLine($"Created on: {CreationDate:yyyy-MM-dd HH:mm:ss}");
            return sb.ToString();
        }
    }
}
