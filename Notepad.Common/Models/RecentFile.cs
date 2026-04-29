using System.Text.Json.Serialization;

namespace Notepad.Common.Models
{
    public class RecentFile
    {
        public string Path { get; set; } = string.Empty;
        public DateTime OpenedAt { get; set; } = DateTime.Now;

        [JsonConstructor]
        public RecentFile() { }

        public RecentFile(string path)
        {
            Path = path;
            OpenedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
