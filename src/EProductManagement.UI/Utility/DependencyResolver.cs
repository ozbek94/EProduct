using Autofac;
using EProductManagement.Data.Contexts;

namespace EProductManagement.UI.Utility
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadRepositories(builder);
        }

        private void LoadRepositories(ContainerBuilder builder)
        {
            // Repositories will be placed here for DI Container
            builder.RegisterType<PostgreSqlContext>().InstancePerLifetimeScope();
            //builder.RegisterType<TestRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterType<UserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
