using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IBoxs.Model
{
    public class Pagamento
    {
        public Pagamento()
        {

        }
        public long? Id { get; set; }
        public float ValorCompra { get; set; }
        /// <summary>
        /// Identificador de token card. (Obrigatório para cartão de crédito)
        /// </summary>
        public string TokenCartao { get; set; }
        /// <summary>
        /// Como aparecerá o pagamento no extrato do cartão (ex: o MERCADOPAGO).
        /// </summary>
        public string Descricao { get; set; }
        public string CodigoCupom { get; set; }
        public float? ValorCupom { get; set; }
        public string MetodoPagamento { get; set; }
        public int Parcelas { get; set; }
        public string TitularCartao { get; set; }
        public StatusPagamento Status { get; set; }
        public string DetalheStatus { get; set; }
        public InformacoesAdicionais? InformacoesAdicionais { get; set; }
        public Pagador Pagador { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public string PublicKey { get; set; }
        public string AccessToken { get; set; }
        public string URLBoleto { get; set; }
        public float TotalPagar { get; set; }
        public string CarrinhoId { get; set; }
        public string TipoEnvio { get; set; }
        public string TipoFrete { get; set; }
        public float ValorFrete { get; set; }
        public string LojaId { get; set; }
        public string BandeiraCartao { get; set; }
        public string QuatroUltimoDigitos { get; set; }
    }


    public enum StatusPagamento
    {
        Pendente = 0,
        Aprovado = 1,
        Autorizado = 2,
        EmProcess = 3,
        EmMediacao = 4,
        Rejeitado = 5,
        Cancelado = 6,
        Estornado = 7,
        Devolucao = 8
    }

    public enum TipoPagamento
    {
        [Description("Na retirada")]
        account_money = 0,
        [Description("Boleto")]
        ticket = 1,
        [Description("Transferência bancária")]
        bank_transfer = 2,
        [Description("Depósito")]
        atm = 3,
        [Description("Cartão de crédito")]
        credit_card = 4,
        [Description("Cartão de débito")]
        debit_card = 5,
        [Description("Cartão pré-pago")]
        prepaid_card = 6,
        [Description("Moeda digital")]
        digital_currency = 7
    }

    public struct InformacoesAdicionais
    {
        public List<Item> Itens { get; set; }

    }

    public struct Item
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? PrecoUnitario { get; set; }
    }
}

