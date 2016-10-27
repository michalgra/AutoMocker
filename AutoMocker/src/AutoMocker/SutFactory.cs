using System;
using Autofac;
using Moq;

namespace AutoMocker
{
    public class SutFactory<T> : IDisposable
    {
        private readonly IContainer _container;

        public SutFactory()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<T>();

            containerBuilder.RegisterSource(new AutoMockRegistrationSource());

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

        public Mock<TDependency> GetMock<TDependency>() where TDependency : class
        {
            return _container.Resolve<Mock<TDependency>>();
        }
    }
}
