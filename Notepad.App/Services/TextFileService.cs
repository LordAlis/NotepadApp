using Notepad.Common.Exceptions;
using Notepad.Common.Services;

namespace Notepad.App.Services
{
    public class TextFileService : FileServiceBase
    {
        public override async Task<string> LoadAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileServiceException("File path cannot be empty.");

            if (!File.Exists(path))
                throw new FileServiceException($"File not found: {path}");

            try
            {
                OnLog($"Loading file: {path}");
                using var reader = new StreamReader(path);
                string content = await reader.ReadToEndAsync().ConfigureAwait(false);
                OnLog($"Loaded {content.Length} characters.");
                return content;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new FileServiceException($"Access denied: {path}", ex);
            }
            catch (IOException ex)
            {
                throw new FileServiceException($"I/O error while reading: {path}", ex);
            }
        }

        public override async Task SaveAsync(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileServiceException("File path cannot be empty.");

            try
            {
                OnLog($"Saving file: {path}");
                using var writer = new StreamWriter(path, append: false);
                await writer.WriteAsync(content).ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
                OnLog($"Saved {content.Length} characters.");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new FileServiceException($"Access denied: {path}", ex);
            }
            catch (IOException ex)
            {
                throw new FileServiceException($"I/O error while writing: {path}", ex);
            }
        }
    }
}
