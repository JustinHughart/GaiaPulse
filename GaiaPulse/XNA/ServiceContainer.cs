using System;
using System.Collections.Generic;

namespace GaiaPulse.XNA
{
    public class ServiceContainer : IServiceProvider
    {
        Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public void AddService<T>(T service)
        {
            Services.Add(typeof(T), service);
        }

        public object GetService(Type serviceType)
        {
            object service;

            Services.TryGetValue(serviceType, out service);

            return service;
        }
    }
}
