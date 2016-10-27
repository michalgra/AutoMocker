namespace AutoMocker.UnitTests
{
    class ComponentWithDependencies
    {
        public IDependency Dependency { get; private set; }

        public ComponentWithDependencies(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}