using FluentAssertions;
using NUnit.Framework;

namespace AutoMocker.UnitTests
{
    [TestFixture]
    public class SutFactoryTests
    {
        [Test]
        public void Should_create_instance_of_component_without_dependencies()
        {
            using (var sut = new SutFactory<ComponentWithoutDependencies>())
            {
                var result = sut.Create();

                result.Should().NotBeNull();
            }
        }

        [Test]
        public void Should_create_instance_of_component_with_dependencies()
        {
            using (var sut = new SutFactory<ComponentWithDependencies>())
            {
                var result = sut.Create();

                result.Should().NotBeNull();
                result.Dependency.Should().NotBeNull();
            }
        }

        [Test]
        public void Should_create_instance_of_component_with_abstract_class_dependency()
        {
            using (var sut = new SutFactory<ComponentWithAbstactClassDependency>())
            {
                var result = sut.Create();

                result.Should().NotBeNull();
                result.Dependency.Should().NotBeNull();
            }
        }

        [Test]
        public void Should_return_mock_used_as_components_dependency()
        {
            using (var sut = new SutFactory<ComponentWithDependencies>())
            {
                var result = sut.Create();
                var mock = sut.GetMock<IDependency>();

                result.Dependency.Should().Be(mock.Object);
            }
        }

        [Test]
        public void Allow_to_use_custom_dependency_implementation()
        {
            using (var sut = new SutFactory<ComponentWithDependencies>())
            {
                var customDependency = new CustomDependency();
                sut.UseDependency<IDependency>(customDependency);
                var result = sut.Create();

                result.Dependency.Should().Be(customDependency);
            }
        }
    }
}
