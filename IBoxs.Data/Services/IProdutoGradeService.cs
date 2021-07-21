using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface IProdutoGradeService
    {
        Task AdicionarGrade(ProdutoGrade ProdutoGrade);
        Task ExcluirProdutoGrade(ProdutoGrade ProdutoGrade);
        Task AlterarProdutoGrade(ProdutoGrade ProdutoGrade);
        Task<List<ProdutoGrade>> ListarProdutoGradePorIdProduto(int IdProduto);
        Task<List<ProdutoGrade>> ListarProdutoGradePorIdCor(int IdCor);
        Task<List<ProdutoGrade>> ListarProdutoGradePorIdTamanho(int IdTamanho);
    }
}
