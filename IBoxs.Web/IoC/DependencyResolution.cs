using StructureMap;
using System;
using System.Collections.Concurrent;

namespace IBoxs.Web.IoC
{
    public static class DependencyResolution
    {
        private static readonly ConcurrentDictionary<string, IContainer> ChildrenContainers = new ConcurrentDictionary<string, IContainer>();

        private static readonly Lazy<Container> Container = new Lazy<Container>(() => new Container());

        /// <summary>
        /// Container do StructureMap para configuração.
        /// </summary>
        public static Container Instance
        {
            get
            {

                {
                    return Container.Value;
                }
            }
        }

        public static IContainer CreateChild(string key)
        {
            var childContainer = Container.Value.CreateChildContainer();
            ChildrenContainers.TryAdd(key, childContainer);

            return childContainer;
        }

        public static void RemoveChild(string key)
        {
            IContainer child;
            if (ChildrenContainers.TryRemove(key, out child))
            {
                child.Dispose();
            }


        }

        private static Container GetChildContainer(string key)
        {
            IContainer child;
            ChildrenContainers.TryGetValue(key, out child);

            return (Container)child;
        }
    }
}