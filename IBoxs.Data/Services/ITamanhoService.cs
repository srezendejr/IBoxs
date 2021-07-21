using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ITamanhoService
    {
        Task AdicionarTamanho(Tamanho Tamanho);
        Task AlterarTamanho(Tamanho Tamanho);
        Task<List<Tamanho>> ListarTamanhos();
        Task<Tamanho> PesquisarPorIdTamanho(int IdTamanho);
    }
}
