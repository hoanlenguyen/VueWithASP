using System.Collections.Concurrent;
using webapi.Extensions.Models;

namespace webapi.Extensions;
public class ExtensionHost : IDisposable
{
    private readonly ConcurrentDictionary<Type, IHostedExtension> _contextExtension = new ConcurrentDictionary<Type, IHostedExtension>();
    private bool disposedValue;

    public bool TryGetExtension<T>(out T extensionT) where T : class, IHostedExtension
    {
        if (_contextExtension.TryGetValue(typeof(T), out var value))
        {
            if (value is T tInstance)
            {
                extensionT = tInstance;
                return true;
            }
        }
        extensionT = default!;
        return false;
    }

    public bool TrySetExtension<T>(T t) where T : class, IHostedExtension
    {
        return _contextExtension.TryAdd(typeof(T), t);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _contextExtension.Clear();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}