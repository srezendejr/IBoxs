using System;
using System.Collections.Generic;
using System.Text;

namespace IBoxs.Model
{
    public class CartaoCredito
    {
        public string Id { get; set; }
        public string UltimosQuatroDigitos { get; }
        public string PrimeirosSeisDigitos { get; }
        public int? AnoExpiracao { get; set; }
        public int? MesExpiracao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public TitularCartao TitularCartao { get; set; }

    }

    public struct TitularCartao
    {
        public string Nome { get; set; }
        public Identificacao? Identificacao { get; set; }
    }

    public struct Identificacao
    {
        public string Type { get { return TipoDoc.ToString(); } set { throw new NotImplementedException("Não pode ser atribuído valor para esta propriedade"); } }
        public string NumeroDocumento { get; set; }

        public TipoDocumento TipoDoc { get; set; }
    }
}
