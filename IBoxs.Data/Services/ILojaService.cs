using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ILojaService
    {
        Task IncluirLoja(Loja Loja);
        Task AlterarLOja(Loja Loja);
        Task ExcluirLoja(Loja Loja);
        Task<List<Loja>> ListarLojas();
        Task<Loja> PesquisarPorIdLoja(int IdLoja);
        Task<Loja> PesquisarLojaPorCodigo(string Id);
        Task<Loja> BuscarLojaPorCNPJCPF(string cNPJCPF);
    }
}
