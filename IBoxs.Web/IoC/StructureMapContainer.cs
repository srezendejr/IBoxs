using IBoxs.Data.Services;
using IBoxs.Negocio;
using StructureMap;

namespace IBoxs.Web.IoC
{
    public class StructureMapContainer
    {
        public static readonly IContainer Container;
        static StructureMapContainer()
        {
            Container = Initializer();
        }

        public static IContainer Initializer()
        {
            var container = new Container(registry =>
            {
                registry.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
                //registry.For<IProdutoService>().Use<ProdutoNegocios>();
                //registry.For<ICarrinhoCompraService>().Use<CarrinhoCompraNegocios>();
                //registry.For<ICalculaFreteService>().Use<CalculaFreteNegocios>();
                //registry.For<IPagamentoService>().Use<PagamentoNegocio>();
                registry.For<ILojaService>().Use<LojaNegocio>();
                registry.For<ILoginService>().Use<LoginNegocio>();
            });
            return container;
        }

        public static T Get<T>()
        {
            return Container.GetInstance<T>();
        }
    }
}