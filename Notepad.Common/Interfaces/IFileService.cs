namespace Notepad.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> LoadAsync(string path);
        Task SaveAsync(string path, string content);
    }
}
