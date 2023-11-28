using Diary.Commands;
using Diary.Enums;
using Diary.Models;
using Diary.Models.Base;
using Diary.Service;
using Diary.ViewModels.Abstract;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class RecordsViewModel : ViewModelBase
    {
        private readonly DiaryService _diaryService = new();

        private ICollection<Record> _records;
        private int id;
        private string title = string.Empty;
        private DateTime reminderDate;
        private string textContent = string.Empty;
        private string titleMod = string.Empty;
        private DateTime reminderDateMod;
        private string textContentMod = string.Empty;
        private RecordType _currentRecordType;
        private bool _isReminderProcessed;
        private bool _isTextRecordProcessed;
        private bool _isReminderProcessedMod;
        private bool _isTextRecordProcessedMod;


        private ICommand? _create;

        private readonly ICommand _addReminderCommand;
        private readonly ICommand _addTextRecordCommand;

        public bool IsReminderProcessed
        {
            get { return _isReminderProcessed; }
            set
            {
                if (_isReminderProcessed != value)
                {
                    _isReminderProcessed = value;
                    OnPropertyChanged(nameof(IsReminderProcessed));
                }
            }
        }

        public bool IsTextRecordProcessed
        {
            get { return _isTextRecordProcessed; }
            set
            {
                if (_isTextRecordProcessed != value)
                {
                    _isTextRecordProcessed = value;
                    OnPropertyChanged(nameof(IsTextRecordProcessed));
                }
            }
        }

        public RecordType CurrentRecordType
        {
            get { return _currentRecordType; }
            set
            {
                if (_currentRecordType != value)
                {
                    _currentRecordType = value;
                    IsReminderProcessed = value == RecordType.Reminder;
                    IsTextRecordProcessed = value == RecordType.TextRecord;
                    Create = value switch
                    {
                        RecordType.TextRecord => _addTextRecordCommand,
                        RecordType.Reminder => _addReminderCommand,
                        _ => null
                    };
                    OnPropertyChanged(nameof(CurrentRecordType));
                }
            }
        }
        public List<string> RecordTypeOptions { get; } = [.. Enum.GetNames(typeof(RecordType))];

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public DateTime ReminderDate
        {
            get { return reminderDate; }
            set
            {
                if (reminderDate != value)
                {
                    reminderDate = value;
                    OnPropertyChanged(nameof(ReminderDate));
                }
            }
        }

        public string TextContent
        {
            get { return textContent; }
            set
            {
                if (textContent != value)
                {
                    textContent = value;
                    OnPropertyChanged(nameof(TextContent));
                }
            }
        }

        public ICommand? Create
        {
            get { return _create; }
            set
            {
                if (_create != value)
                {
                    _create = value;
                    OnPropertyChanged(nameof(Create));
                }
            }
        }

        public ICommand ShowAll { get; }
        public ICommand ShowTextRecords { get; }
        public ICommand ShowReminders { get; }

        public ICollection<Record> Records
        {
            get { return _records; }
            set
            {
                _records = value;
                OnPropertyChanged(nameof(Records));
            }
        }


        public bool IsReminderProcessedMod
        {
            get { return _isReminderProcessedMod; }
            set
            {
                if (_isReminderProcessedMod != value)
                {
                    _isReminderProcessedMod = value;
                    OnPropertyChanged(nameof(IsReminderProcessedMod));
                }
            }
        }

        public bool IsTextRecordProcessedMod
        {
            get { return _isTextRecordProcessedMod; }
            set
            {
                if (_isTextRecordProcessedMod != value)
                {
                    _isTextRecordProcessedMod = value;
                    OnPropertyChanged(nameof(IsTextRecordProcessedMod));
                }
            }
        }

        public string TitleMod
        {
            get { return titleMod; }
            set
            {
                if (titleMod != value)
                {
                    titleMod = value;
                    OnPropertyChanged(nameof(TitleMod));
                }
            }
        }

        public DateTime ReminderDateMod
        {
            get { return reminderDateMod; }
            set
            {
                if (reminderDateMod != value)
                {
                    reminderDateMod = value;
                    OnPropertyChanged(nameof(ReminderDateMod));
                }
            }
        }

        public string TextContentMod
        {
            get { return textContentMod; }
            set
            {
                if (textContentMod != value)
                {
                    textContentMod = value;
                    OnPropertyChanged(nameof(TextContentMod));
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public ICommand Delete { get; }

        public ICommand Find { get; }

        public ICommand Update { get; }

        public ICommand Download { get; }

        public RecordsViewModel()
        {
            _records = _diaryService.Records;

            Update = new RelayCommand(
                executeAction => {
                    Record updatedRecord;

                    if (IsReminderProcessedMod)
                    {
                        updatedRecord = new Reminder(Id, TitleMod, ReminderDateMod);
                    }
                    else if (IsTextRecordProcessedMod)
                    {
                        updatedRecord = new TextRecord(Id, TitleMod, TextContentMod);
                    }
                    else
                    {
                        return;
                    }

                    if (_diaryService.TryUpdateRecord(updatedRecord))
                    {
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    };
                },
                canExecuteFunc => true);

            Find = new RelayCommand(
                executeAction =>
                {
                    var res = _diaryService.GetRecordById(Id);
                    if (res is TextRecord textRecord)
                    {
                        TitleMod = textRecord.Title;
                        TextContentMod = textRecord.TextContent;
                        IsReminderProcessedMod = false;
                        IsTextRecordProcessedMod = true;
                    }
                    else if (res is Reminder rem)
                    {
                        TitleMod = rem.Title;
                        ReminderDateMod = rem.ReminderDate;
                        IsReminderProcessedMod = true;
                        IsTextRecordProcessedMod = false;
                    }
                    else
                    {
                        MessageBox.Show("Not Found");
                    }
                },
                canExecuteFunc => true);

            Delete = new RelayCommand(
                executeAction =>
                {
                    if (_diaryService.TryRemoveRecord(Id))
                    {
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    };

                    TitleMod = string.Empty;
                    TextContentMod = string.Empty;
                    ReminderDateMod = new DateTime();
                    IsReminderProcessedMod = false;
                    IsTextRecordProcessedMod = false;
                    Id = 0;
                },
                canExecuteFunc => true);

            ShowAll = new RelayCommand(
                executeAction => {
                    Records = _diaryService.Records;
                },
                canExecuteFunc => true);

            ShowReminders = new RelayCommand(
                executeAction => {
                    Records = _diaryService.Reminders;
                },
                canExecuteFunc => true);

            ShowTextRecords = new RelayCommand(
                executeAction => {
                    Records = _diaryService.TextRecords;
                },
                canExecuteFunc => true);

            _addReminderCommand = new RelayCommand(
                executeAction => {
                    _diaryService.CreateReminder(Title, ReminderDate);
                },
                canExecuteFunc => true);

            _addTextRecordCommand = new RelayCommand(
                executeAction => {
                    _diaryService.CreateTextRecord(Title, TextContent);
                },
                canExecuteFunc => true);

            Download = new RelayCommand(
                executeAction: async (param) => {
                    await DownloadRecords();
                },
                canExecuteFunc => true);

        }

        public async Task DownloadRecords()
        {
            DriveInfo dDrive = new("D:");
            string filePath = Path.Combine(dDrive.RootDirectory.FullName, "diary.txt");
            using StreamWriter writer = new(filePath);
            StringBuilder sb = new();
            foreach (var record in _records)
            {
                sb.AppendLine(record.DisplayInfo());
            }
            await writer.WriteAsync(sb.ToString());
        }

    }
}
