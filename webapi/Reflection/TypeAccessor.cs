using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using PropertyCache = System.Collections.Generic.IReadOnlyList<webapi.Reflection.PropertyAccessor>;

namespace webapi.Reflection
{
    public static class TypeAccessor
    {
        private static readonly ConcurrentDictionary<Type, PropertyCache> _cache = new ConcurrentDictionary<Type, PropertyCache>();

        public static PropertyCache GetPropertyCache(this Type type)
        {
            return _cache.GetOrAdd(type, (t) =>
            {
                var propertyCache = new List<PropertyAccessor>();

                var properties = t.GetProperties();
                foreach (var property in properties)
                {
                    propertyCache.Add(new PropertyAccessor(property));
                }

                return propertyCache;
            });
        }

        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, MethodInfo>> _asyncMethodsByType = new ConcurrentDictionary<Type, ConcurrentDictionary<string, MethodInfo>>();
        public static async Task<object?> InvokeAsyncGeneric(this Type type, string methodName, Type[] genricTypes, params object?[] args)
        {
            MethodInfo? methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!;

            if (methodInfo != null)
            {
                var keyType = GetGenericTypeKey(genricTypes);

                var methodsByName = _asyncMethodsByType.GetOrAdd(keyType, (t) => new ConcurrentDictionary<string, MethodInfo>());

                var method = methodsByName.GetOrAdd(methodName, (m) => methodInfo!.MakeGenericMethod(genricTypes));

                if (method != null)
                {
                    var task = (Task)method.Invoke(null, args)!;

                    await task.ConfigureAwait(false);

                    var result = task.GetType().GetProperty("Result");
                    if (result != null)
                    {
                        return result.GetValue(task);
                    }
                }
                else
                {
                    Debug.Fail("GenericMethod Creation Failed");
                }
            }

            return null;
        }

        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, MethodInfo>> _methodsByType = new ConcurrentDictionary<Type, ConcurrentDictionary<string, MethodInfo>>();
        public static object? InvokeGeneric(this Type type, string methodName, Type genricType, params object?[] args)
        {
            return type.InvokeGeneric(methodName, new Type[] { genricType }, args);
        }

        public static object? InvokeGeneric(this Type type, string methodName, Type[] genricTypes, params object?[] args)
        {
            MethodInfo? methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!;

            if (methodInfo != null)
            {
                var keyType = GetGenericTypeKey(genricTypes);

                var methodsByName = _methodsByType.GetOrAdd(keyType, (t) => new ConcurrentDictionary<string, MethodInfo>());

                var method = methodsByName.GetOrAdd(methodName, (m) => methodInfo!.MakeGenericMethod(genricTypes));

                if (method != null)
                {
                    return method.Invoke(null, args)!;
                }
                else
                {
                    Debug.Fail("GenericMethod Creation Failed");
                }
            }

            return null;
        }

        private static Type GetGenericTypeKey(Type[] genricTypes)
        {
            if (genricTypes.Length == 1)
            {
                return genricTypes[0];
            }
            if (genricTypes.Length == 2)
            {
                return (genricTypes[0], genricTypes[1]).GetType();
            }
            if (genricTypes.Length == 3)
            {
                return (genricTypes[0], genricTypes[1], genricTypes[2]).GetType();
            }
            if (genricTypes.Length == 4)
            {
                return (genricTypes[0], genricTypes[1], genricTypes[2], genricTypes[3]).GetType();
            }

            throw new ArgumentOutOfRangeException($"Must be between 1 and 4 generic types ({genricTypes.Length})");
        }
    }
}
