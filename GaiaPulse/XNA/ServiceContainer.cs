using System;
using System.Collections.Generic;

namespace GaiaPulse.XNA
{
    public class ServiceContainer : IServiceProvider
    {
        Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void AddService<T>(T service)
        {
            _services.Add(typeof(T), service);
        }

        public object GetService(Type serviceType)
        {
            object service;

            _services.TryGetValue(serviceType, out service);

            return service;
        }
    }
}