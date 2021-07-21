using IBoxs.Model;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ICalculaFreteService
    {
        Task<Frete> CalculaFreteValor(CalculaFretePrazo calcFrete);
    }
}
