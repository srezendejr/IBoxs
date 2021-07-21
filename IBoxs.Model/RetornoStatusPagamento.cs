using IBoxs.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IBoxs.Model
{
    public class RetornoStatusPagamento
    {
        public long? IdPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public StatusRetornoPagamento DetalheStatusPagamento { get; set; }
        public string DescricaoCompra { get; set; }
        [DisplayName("Status do pagamento")]
        public string TextoStatusPagamento
        {
            get
            {
                return string.Format(DetalheStatusPagamento.ObterDescricao(), DescricaoCompra);
            }
            set
            {
                throw new NotImplementedException("Este campo não pode ser implementado");
            }
        }
        public string Url { get; set; }
    }

    public enum StatusRetornoPagamento
    {
        [Description("Verifique o número do cartão")]
        cc_rejected_bad_filled_card_number = 0,

        [Description("Verifique a data de validade.")]
        cc_rejected_bad_filled_date = 1,

        [Description("Revise os dados.")]
        cc_rejected_bad_filled_other = 2,

        [Description("Verifique o código de segurança do cartão.")]
        cc_rejected_bad_filled_security_code = 3,

        [Description("Não foi possível processar seu pagamento.")]
        cc_rejected_blacklist = 4,

        [Description("Você deve autorizar antes na operadora do cartão pagamento do valor da compra.")]
        cc_rejected_call_for_authorize = 5,

        [Description("Ligue para operadora do cartão ativar seu cartão ou usar outra forma de pagamento. O telefone está na parte de trás do seu cartão.")]
        cc_rejected_card_disabled = 6,
        [Description("Não foi possível processar seu pagamento.")]
        cc_rejected_card_error = 7,
        [Description("Você já efetuou um pagamento por esse valor. Se você precisar pagar novamente, use outro cartão ou outra forma de pagamento.")]
        cc_rejected_duplicated_payment = 8,
        [Description("Seu pagamento foi recusado. Escolha outro método de pagamento, recomendamos em dinheiro.")]
        cc_rejected_high_risk = 9,
        [Description("Você payment_method_idnão possui fundos suficientes.")]
        cc_rejected_insufficient_amount = 10,
        [Description("A operadora não processa pagamentos parcelados.")]
        cc_rejected_invalid_installments = 11,
        [Description("Você atingiu o limite de tentativas permitidas. Escolha outro cartão ou outro método de pagamento.")]
        cc_rejected_max_attempts = 12,
        [Description("Operadora não processou o pagamento.")]
        cc_rejected_other_reason = 13,
        [Description("Pronto, seu pagamento foi aprovado! No resumo, você verá a cobrança do valor como {0}.")]
        accredited = 14,
        [Description("Aguardando pagamento. Visualize o seu boleto no link abaixo")]
        pending_waiting_payment = 15

    }    
}
