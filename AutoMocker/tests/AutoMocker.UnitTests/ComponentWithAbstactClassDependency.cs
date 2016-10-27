namespace AutoMocker.UnitTests
{
    class ComponentWithAbstactClassDependency
    {
        public AbstractDependency Dependency { get; private set; }

        public ComponentWithAbstactClassDependency(AbstractDependency dependency)
        {
            Dependency = dependency;
        }
    }
}