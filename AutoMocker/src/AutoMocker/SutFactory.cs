using System;
using Autofac;

namespace AutoMocker
{
    public class SutFactory<T> : IDisposable
    {
        private readonly IContainer _container;

        public SutFactory()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<T>();

            _container = containerBuilder.Build();
        }

        ~SutFactory()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public T Create()
        {
            return _container.Resolve<T>();
        }
    }
}
