using Diary.Models;
using Diary.Models.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Controls;

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
            //new Reminder(1, "Meeting Reminder 1", DateTime.Now.AddDays(1)),
            //new Reminder(2, "Meeting Reminder 2", DateTime.Now.AddDays(2)),
            //new TextRecord(5, "Note 1", "This is a sample text note 1."),
            //new TextRecord(6, "Note 2", "This is a sample text note 2."),
        ];

        public ObservableCollection<Record> Reminders => GetRecordsOfType<Reminder>();
        public ObservableCollection<Record> TextRecords => GetRecordsOfType<TextRecord>();
        public ObservableCollection<Record> Records
        {
            get { return _records; }
            private set
            {
                _records = value;
                OnPropertyChanged(nameof(Records));
                OnPropertyChanged(nameof(Reminders));
                OnPropertyChanged(nameof(TextRecords));
            }
        }

        public ObservableCollection<Record> GetRecordsOfType<T>() where T : Record
        {
            return new ObservableCollection<Record>(Records
                .Where(record => record is T)
                .ToList());
        }
        public void CreateReminder(string title, DateTime reminderDate)
        {
            ++_globalId;
            var reminder = new Reminder(_globalId, title, reminderDate);
            Records.Add(reminder);
        }

        public void CreateTextRecord(string title, string textContent)
        {
            ++_globalId;
            var textRecord = new TextRecord(_globalId, title, textContent);
            Records.Add(textRecord);
        }

        public bool TryRemoveRecord(int recordId)
        {
            var record = Records.FirstOrDefault(x => x.Id == recordId);
            if (record != null)
            {
                Records.Remove(record);
                return true;
            }
            return false;
        }

        public bool TryUpdateRecord(Record updatedRecord)
        {
            var existingRecord = Records.FirstOrDefault(x => x.Id == updatedRecord.Id);
            if (existingRecord is not null)
            {
                Records[Records.IndexOf(existingRecord)] = updatedRecord;
                return true;
            }
            return false;
        }

        public Record? GetRecordById(int id)
        {
            return _records.FirstOrDefault( x => x.Id == id);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    }
}
