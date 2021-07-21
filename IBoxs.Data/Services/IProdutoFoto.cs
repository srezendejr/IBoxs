using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface IProdutoFotoService
    {
        Task AdicionarFoto(ProdutoFoto Foto);
        Task ExcluirFoto(ProdutoFoto Foto);
        Task<List<ProdutoFoto>> ListarProdutoFoto();
        Task<List<ProdutoFoto>> ListarFotosPorIdProduto(int IdProduto);
    }
}
