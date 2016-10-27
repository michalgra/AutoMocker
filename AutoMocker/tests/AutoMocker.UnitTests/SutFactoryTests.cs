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
    }
}
