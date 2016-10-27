using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Moq;

namespace AutoMocker
{
    class AutoMockRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var noComponents = Enumerable.Empty<IComponentRegistration>();

            var swt = service as IServiceWithType;
            if (swt == null)
            {
                return noComponents;
            }

            var serviceType = swt.ServiceType;
            if (!serviceType.IsInterface)
            {
                return noComponents;
            }

            if (registrationAccessor(service).Any())
            {
                return noComponents;
            }

            var registrationCreator = CreateMockRegistrationsMethod.MakeGenericMethod(serviceType);

            return (IEnumerable<IComponentRegistration>) registrationCreator.Invoke(null, new object[] {service});
        }

        public bool IsAdapterForIndividualComponents => false;

        static readonly MethodInfo CreateMockRegistrationsMethod = typeof(AutoMockRegistrationSource).GetMethod("CreateMockRegistrations", BindingFlags.Static | BindingFlags.NonPublic);

        // invoked using reflection
        // ReSharper disable once UnusedMember.Local
        private static IEnumerable<IComponentRegistration> CreateMockRegistrations<T>(Service providedService) where T : class
        {
            yield return RegistrationBuilder.ForDelegate(
                    (c, p) => new Mock<T>())
                .AsSelf()
                .InstancePerLifetimeScope()
                .CreateRegistration();

            yield return RegistrationBuilder.ForDelegate(
                    (c, p) =>
                    {
                        var context = c.Resolve<IComponentContext>();
                        var mock = context.Resolve<Mock<T>>();
                        return mock.Object;
                    }
                ).As(providedService)
                .CreateRegistration();
        }
    }
}