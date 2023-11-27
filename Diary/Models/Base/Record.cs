namespace Diary.Models.Base
{
    public abstract class Record(int id, string title, DateTime creationDate)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public DateTime CreationDate { get; set; } = creationDate;

        public abstract string DisplayInfo();
    }
}
