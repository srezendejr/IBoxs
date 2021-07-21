using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IBoxs.Model
{
    public class CarrinhoCompra
    {
        public string Id { get; set; }
        public DateTime DataOperacao { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdProdutoGrade { get; set; }
        public string ReferenciaProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int QuantidadeProduto { get; set; }
        public string TamanhoSelecionado { get; set; }
        public string CorSelecionada { get; set; }
        public StatusCarrinho Aberto { get; set; }
        public Int64 Indice { get; set; }
        [NotMapped]
        public List<string> Imagens { get; set; }
        public int IdLoja { get; set; }
        public string TokenProdutos { get; set; }
        public string NCM { get; set; }
        public virtual Loja Loja { get; set; }
        public virtual ProdutoGrade ProdutoGrade { get; set; }
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [NotMapped]
        public int QtdEstoque { get; set; }
        [NotMapped]
        public bool SemEstoque { get; set; }
    }

    public enum StatusCarrinho
    {
        Fechado = 0,
        Aberto = 1
    }
}
