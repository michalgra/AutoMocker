using Moq;
using NUnit.Framework;

namespace AutoMocker.NUnit
{
    public class TestFixtureBase<T>
    {
        private SutFactory<T> _sutFactory;

        [SetUp]
        public virtual void InitTest()
        {
            _sutFactory = new SutFactory<T>();
        }

        [TearDown]
        public virtual void CleanupTest()
        {
            _sutFactory.Dispose();
        }

        public T Create()
        {
            return _sutFactory.Create();
        }

        public Mock<TDependency> GetMock<TDependency>() where TDependency : class
        {
            return _sutFactory.GetMock<TDependency>();
        }

        public void UseDependency<TDependency>(TDependency dependency) where TDependency : class
        {
            _sutFactory.UseDependency(dependency);
        }
    }
}
