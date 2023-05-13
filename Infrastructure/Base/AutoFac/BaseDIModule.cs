using Autofac;
using Infrastructure.AutoFac.FlagInterface;

namespace Infrastructure.AutoFac
{
    public abstract class BaseDIModule<T> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(T).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IScope).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(ITransient).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(ISingleton).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();


            builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IScope).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(ITransient).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(ISingleton).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
