using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ICarrinhoCompraService
    {
        Task CriarCarrinho(CarrinhoCompra CarrinhoCompra);
        Task AdicionarItem(CarrinhoCompra Item);
        Task ExcluirItem(CarrinhoCompra Item);
        Task AlterarItem(CarrinhoCompra Item);
        Task<List<CarrinhoCompra>> ListarItens();
    }
}
