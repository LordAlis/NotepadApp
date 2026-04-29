using Notepad.Common.Interfaces;

namespace Notepad.Common.Services
{
    public abstract class FileServiceBase : IFileService, IDisposable
    {
        private bool _disposed;

        public event Action<string>? LogMessage;

        public abstract Task<string> LoadAsync(string path);
        public abstract Task SaveAsync(string path, string content);

        protected void OnLog(string message)
        {
            LogMessage?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public virtual void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            LogMessage = null;
            GC.SuppressFinalize(this);
        }
    }
}
