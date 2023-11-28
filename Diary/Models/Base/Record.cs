namespace Diary.Models.Base
{
    public abstract class Record(int id, string title)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public abstract string DisplayInfo();
    }
}
