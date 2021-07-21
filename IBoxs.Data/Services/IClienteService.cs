using IBoxs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface IClienteService
    {
        Task IncluirCliente(Cliente Cliente);
        Task AlterarCliente(Cliente Cliente);
        Task<List<Cliente>> ListarClientes();
        Task<Cliente> PesquisarPorIdCliente(int IdCliente);
    }
}
