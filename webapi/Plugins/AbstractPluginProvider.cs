using Microsoft.Extensions.Options;
using System.Reflection;

namespace webapi.Plugins;
public abstract class AbstractPluginProvider<T, U> where U : Attribute
{
    protected readonly PluginProviderSettings _pluginProviderSettings;
    protected IServiceProvider _serviceProvider;
    protected ILogger<AbstractPluginProvider<T, U>> _logger;
    protected static Type _pluginType = typeof(T);
    protected static Type AttributeType = typeof(U);

    protected virtual Type PluginType
    {
        get
        {
            return _pluginType;
        }
    }

    private static bool _loaded = false;
    private static readonly object _lock = new object();

    public AbstractPluginProvider(ILogger<AbstractPluginProvider<T, U>> logger, IServiceProvider serviceProvider, IOptions<PluginProviderSettings> pluginProviderSettings)
    {
        _pluginProviderSettings = pluginProviderSettings.Value;
        _serviceProvider = serviceProvider;
        _logger = logger;

        _pluginProviderSettings = pluginProviderSettings.Value;

        try
        {
            if (!_loaded)
            {
                lock (_lock)
                {
                    if (!_loaded)
                    {
                        LoadPlugins();
                        _loaded = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load Visitors");
        }
    }

    private void LoadPlugins()
    {
        string assemblyPath = Path.GetDirectoryName(_pluginProviderSettings?.AssemblyPath ?? Assembly.GetExecutingAssembly().Location)!;
        var pluginAssemblyLoadContext = new PluginAssemblyLoadContext(assemblyPath);

        if (assemblyPath.EndsWith(".dll"))
        {
            LoadPlugins(pluginAssemblyLoadContext, assemblyPath);
        }
        else
        {
            foreach (string file in Directory.GetFiles(assemblyPath, "Schrole.Edu.*.dll").Where(f => !f.Contains("Schrole.Edu.Plugins.dll") && !f.Contains("Precompiled")))
            {
                try
                {
                    LoadPlugins(pluginAssemblyLoadContext, file);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to load Visitors from {file}");
                }
            }
        }
    }

    private void LoadPlugins(PluginAssemblyLoadContext pluginLoadContext, string file)
    {
        Assembly? assembly = pluginLoadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(file));

        foreach (Type pluginType in assembly?.GetTypes() ?? Enumerable.Empty<Type>())
        {
            foreach (var pluginInstanceAttribute in pluginType.GetCustomAttributes<U>())
            {
                AddPlugin(pluginType, pluginInstanceAttribute);
            }
        }
    }

    protected abstract void AddPlugin(Type visitor, U pluginInstanceAttribute);
}