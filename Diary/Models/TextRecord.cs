using Diary.Models.Base;
using System.Text;

namespace Diary.Models
{
    public class TextRecord(int id, string title, string textContent) : Record(id, title)
    {
        public string TextContent { get; set; } = textContent;

        private static readonly char[] separator = [' ', '\t', '\r', '\n'];

        private static int CountWords(string text)
        {
            string[] words = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public override string DisplayInfo()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Text Note - {Title}");
            sb.AppendLine($"Text Content: {TextContent}");
            sb.AppendLine($"Created on: {CreationDate:yyyy-MM-dd HH:mm:ss}");

            int wordCount = CountWords(TextContent);
            sb.AppendLine($"Word Count: {wordCount}");

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Text Note - {Title}, id - {Id}");
            sb.AppendLine($"Content: {TextContent}");
            sb.AppendLine($"Created on: {CreationDate:yyyy-MM-dd HH:mm:ss}");
            return sb.ToString();
        }
    }
}
