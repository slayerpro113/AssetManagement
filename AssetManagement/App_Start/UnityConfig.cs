using Data.DataContext;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;
using Service;
using System;

using Unity;
using Unity.AspNet.Mvc;

namespace AssetManagement
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<IDataContext, AssetManagementEntities >(new PerRequestLifetimeManager());

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            container
                .RegisterType<IRepository<Category>, Repository<Category>>();
            container
                .RegisterType<IRepository<Employee>, Repository<Employee>>();
            container
                .RegisterType<IRepository<RoleEmployee>, Repository<RoleEmployee>>();
            container
                .RegisterType<IRepository<Role>, Repository<Role>>();
            container
                .RegisterType<IRepository<Product>, Repository<Product>>();
            container
                .RegisterType<IRepository<Asset>, Repository<Asset>>();
            container
                .RegisterType<IRepository<History>, Repository<History>>();
            container
                .RegisterType<IRepository<PoRequest>, Repository<PoRequest>>();
            container
                .RegisterType<IRepository<Quote>, Repository<Quote>>();
            container
                .RegisterType<IRepository<Order>, Repository<Order>>();

            //Service layer
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IRoleEmployeeService, RoleEmployeeService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IAssetService, AssetService>();
            container.RegisterType<IHistoryService, HistoryService>();
            container.RegisterType<IPoRequestService, PoRequestService>();
            container.RegisterType<IQuoteService, QuoteService>();
            container.RegisterType<IOrderService, OrderService>();

        }
    }
}