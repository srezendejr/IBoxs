using IBoxs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ICorService
    {
        Task AsicionarCor(Cor Cor);
        Task AlterarCor(Cor Cor);
        Task<List<Cor>> ListarCores();
        Task<Cor> PesquisarPorIdCor(int IdCor);
    }
}
