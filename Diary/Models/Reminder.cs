using Diary.Models.Base;
using System.Text;

namespace Diary.Models
{
    public class Reminder(int id, string title, DateTime reminderDate) : Record(id, title)
    {
        public DateTime ReminderDate { get; set; } = reminderDate;

        public override string DisplayInfo()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Reminder - {Title}");
            sb.AppendLine($"Reminder Date: {ReminderDate:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Updated on: {LastUpdated:yyyy-MM-dd HH:mm:ss}");

            // Additional information for DisplayInfo
            if (DateTime.Now > ReminderDate)
            {
                sb.AppendLine("Status: Overdue");
            }
            else if (DateTime.Now.AddDays(1) > ReminderDate)
            {
                sb.AppendLine("Status: Upcoming");
            }
            else
            {
                sb.AppendLine("Status: Normal");
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Reminder - {Title}, id - {Id}");
            sb.AppendLine($"Reminder Date: {ReminderDate:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Updated on: {LastUpdated:yyyy-MM-dd HH:mm:ss}");
            return sb.ToString();
        }
    }
}
