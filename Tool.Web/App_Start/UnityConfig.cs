using System.Web.Mvc;
using Tool.Web.Repository;
using Unity;
using Unity.Mvc5;

namespace Tool.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IDocumentManager, DocumentManager>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}