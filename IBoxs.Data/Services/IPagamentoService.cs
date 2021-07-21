using IBoxs.Model;
using System.Threading.Tasks;

namespace IBoxs.Data.Services
{
    public interface IPagamentoService
    {
        Task<Pagamento> EfetuarPagamento(Pagamento Pagamento);

        void SetTokenAcesso(string TokenAcesso);

        RetornoStatusPagamento RecuperarPagamento(long IdPagamento);
        Task FinalizarCompra(Pagamento pagamento);
    }
}
