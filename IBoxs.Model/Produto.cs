using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Model
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string CodigoReferencia { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal ValorVenda { get; set; }
        public bool TamanhoUnico { get; set; }
        public bool CorUnica { get; set; }
        public int IdLoja { get; set; }
        public virtual Loja Loja { get; set; }
    }

    public class ProdutoFoto
    {
        public int IdProdutoFoto { get; set; }
        public int IdProduto { get; set; }
        public string Foto { get; set; }
        public virtual Produto Produto { get; set; }
    }

    public class ProdutoGrade
    {
        public int IdProdutoGrade { get; set; }
        public int IdProduto { get; set; }
        public string NomeCor { get; set; }
        public string NomeTamanho { get; set; }
        public string QtdEstoque { get; set; }
        public int IdCor { get; set; }
        public int IdTamanho { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        public virtual Cor Cor { get; set; }
    }
}
