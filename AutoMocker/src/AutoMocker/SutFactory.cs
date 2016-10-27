using System;
using Autofac;
using Moq;

namespace AutoMocker
{
    public class SutFactory<T> : IDisposable
    {
        private IContainer _container;
        private readonly ContainerBuilder _containerBuilder = new ContainerBuilder();

        public SutFactory()
        {
            _containerBuilder.RegisterType<T>();
            _containerBuilder.RegisterSource(new AutoMockRegistrationSource());
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
                _container?.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public T Create()
        {
            _container = _containerBuilder.Build();
            return _container.Resolve<T>();
        }

        public Mock<TDependency> GetMock<TDependency>() where TDependency : class
        {
            return _container.Resolve<Mock<TDependency>>();
        }

        public void UseDependency<TDependency>(TDependency dependency) where TDependency : class
        {
            _containerBuilder.RegisterInstance(dependency)
                .As<TDependency>();
        }
    }
}
