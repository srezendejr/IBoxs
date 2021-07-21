using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface ILoginService
    {
        Task<bool> VerificaLogin(string id, string password);
    }
}
