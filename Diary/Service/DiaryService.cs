using Diary.Models;
using Diary.Models.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Diary.Service
{
    public class DiaryService : INotifyPropertyChanged
    {
        private static readonly Lazy<DiaryService> instance = new(() => new DiaryService());

        private int _globalId = 0;

        public static DiaryService Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ObservableCollection<Record> _records =
        [
            new Reminder(1, "Meeting Reminder 1", DateTime.Now.AddDays(1), DateTime.Now),
            new Reminder(2, "Meeting Reminder 2", DateTime.Now.AddDays(2), DateTime.Now),
            new TaskList(3, "Daily Tasks 1", new List<string> { "Complete project tasks", "Attend team meeting" }, DateTime.Now),
            new TaskList(4, "Daily Tasks 2", new List<string> { "Review emails", "Prepare presentation" }, DateTime.Now),
            new TextRecord(5, "Note 1", "This is a sample text note 1.", DateTime.Now),
            new TextRecord(6, "Note 2", "This is a sample text note 2.", DateTime.Now),
        ];

        public ObservableCollection<Record> Reminders => GetRecordsOfType<Reminder>();
        public ObservableCollection<Record> TextRecords => GetRecordsOfType<TextRecord>();
        public ObservableCollection<Record> TaskLists => GetRecordsOfType<TaskList>();

        public ObservableCollection<Record> Records
        {
            get { return _records; }
            private set
            {
                _records = value;
                OnPropertyChanged(nameof(Records));
                OnPropertyChanged(nameof(Reminders));
                OnPropertyChanged(nameof(TextRecords));
                OnPropertyChanged(nameof(TaskLists));
            }
        }


        public ObservableCollection<Record> GetRecordsOfType<T>() where T : Record
        {
            return new ObservableCollection<Record>(Records
                .Where(record => record is T)
                .ToList());
        }
        public void CreateReminder(string title, DateTime reminderDate, DateTime creationDate)
        {
            ++_globalId;
            var reminder = new Reminder(_globalId, title, reminderDate, creationDate);
            _ = Records.Append(reminder);
        }

        public void CreateTaskList(string title, List<string> tasks, DateTime creationDate)
        {
            ++_globalId;
            var taskList = new TaskList(_globalId, title, tasks, creationDate);
            _ = Records.Append(taskList);
        }

        public void CreateTextRecord(string title, string textContent, DateTime creationDate)
        {
            ++_globalId;
            var textRecord = new TextRecord(_globalId, title, textContent, creationDate);
            _ = Records.Append(textRecord);
        }

        public bool TryRemoveContact(int recordId)
        {
            var record = Records.FirstOrDefault(x => x.Id == recordId);
            if (record != null)
            {
                Records.Remove(record);
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    }
}
