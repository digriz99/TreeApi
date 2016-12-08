using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using TreeApi.DbModels;
using TreeApi.Providers;
using Unity.WebApi;

namespace TreeApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container
                .RegisterType<ITreeProvider, TreeProvider>()
                .RegisterType<DbContext, TreeApiContext>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}