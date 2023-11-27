using Diary.Commands;
using Diary.Models.Base;
using Diary.Service;
using Diary.ViewModels.Abstract;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class RecordsViewModel : ViewModelBase
    {
        private readonly DiaryService _diaryService = new();

        private ICollection<Record> _records;

        public ICommand ShowAll { get; }
        public ICommand ShowTextRecords { get; }
        public ICommand ShowTaskLists { get; }
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

        public RecordsViewModel()
        {
            _records = _diaryService.Records;

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

            ShowTaskLists = new RelayCommand(
                executeAction => {
                    Records = _diaryService.TaskLists;
                },
                canExecuteFunc => true);

            ShowTextRecords = new RelayCommand(
                executeAction => {
                    Records = _diaryService.TextRecords;
                },
                canExecuteFunc => true);
        }
    }
}
