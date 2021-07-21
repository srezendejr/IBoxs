using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ICampanhaService
    {
        Task CadastrarCampanha(Campanha Campanha);
        Task AlterarCampanha(Campanha Campanha);
        Task FinalizarCampanha(int IdCampanha);
        Task AdicionarProduto(CampanhaProduto CampanhaProduto);
        Task<List<Campanha>> ListarCampanhas();
        Task<Campanha> PesquisarPorIdCampanha(int IdCampanha);
    }
}
