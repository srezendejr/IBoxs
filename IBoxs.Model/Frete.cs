using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IBoxs.Model
{
    public class Frete
    {
        public int Codigo { get; set; }
        public string Valor { get; set; }
        public string PrazoEntrega { get; set; }
        public string ValorMaoPropria { get; set; }
        public string ValorAvisoRecebimento { get; set; }
        public string ValorValorDeclarado { get; set; }
        public string EntregaDomiciliar { get; set; }
        public string EntregaSabado { get; set; }
        public string Erro { get; set; }
        public string MsgErro { get; set; }
        public string ValorSemAdicionais { get; set; }
        public string obsFim { get; set; }
        public string DataMaxEntrega { get; set; }
        public string HoraMaxEntrega { get; set; }
    }

    public class CalculaFretePrazo
    {
        public string nCdEmpresa { get; set; }
        public string sDsSenha { get; set; }
        public string nCdServico { get; set;  }
        public string sCepOrigem { get; set; }
        public string sCepDestino { get; set; }
        public string nVlPeso { get; set; }
        public int nCdFormato { get { return (int)Formato; }  set { throw new InvalidOperationException(); } }
        public Formato Formato { get; set; }
        public decimal nVlComprimento { get; set; }
        public decimal nVlAltura { get; set; }
        public decimal nVlLargura { get; set; }
        public decimal nVlDiametro { get; set; }
        public string sCdMaoPropria { get; set; }
        public decimal nVlValorDeclarado { get; set; }
        public string sCdAvisoRecebimento { get; set; }

    }

    public enum Formato
    {
        Formatocaixapacote = 1,
        Formatoroloprisma = 2,
        Envelope = 3
    }

    public enum Servico
    {
        [Description("SEDEX à vista")]
        SEDEXavista = 04014,

        [Description("PAC à vista")]
        PACavista = 04510,

        [Description("SEDEX 12 ( à vista)")]
        SEDEX12avista = 04782,

        [Description("SEDEX 10 (à vista)")]
        SEDEX10avista = 04790,

        [Description("SEDEX Hoje à vista")]
        SEDEXHojeavista = 04804

    }
}
