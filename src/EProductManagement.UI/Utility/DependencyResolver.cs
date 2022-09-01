using Autofac;
using EProductManagement.Data.Contexts;
using EProductManagement.Data.Repositories;
using EProductManagement.Data.Transactions;
using EProductManagement.Domain.Services;

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
            builder.RegisterType<EProductRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StockTransactionRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ProductBalanceRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ProductBalanceService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EProductService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<TransactionScopeService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StockTransactionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<HttpService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();


            //builder.RegisterType<UserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //services.AddScoped<IEProductRepository, EProductRepository>();
            //services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
            //services.AddScoped<IProductBalanceRepository, ProductBalanceRepository>();
            //services.AddScoped<IProductBalanceService, ProductBalanceService>();
            //services.AddScoped<IEProductService, EProductService>();
            //services.AddScoped<ITransactionService, TransactionScopeService>();
            //services.AddScoped<IStockTransactionService, >();
        }
    }
}
