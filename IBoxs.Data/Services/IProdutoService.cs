using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface IProdutoService
    {
        Task IncluirProduto(Produto Produto);
        Task AlterarProduto(Produto Produto);
        Task<List<Produto>> ListarProdutos();
        Task<Produto> PesquisarPorIdProduto(int IdProduto);

    }
}
