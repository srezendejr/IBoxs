using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Model
{
    public class Campanha
    {
        public int IdCampanha { get; set; }
        public string NomeCampnha { get; set; }
        public string TokenCampanha { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }

    public class CampanhaProduto
    {
        public int IdCampanhaProduto { get; set; }
        public int IdProduto { get; set; }
        public int IdCampanha { get; set; }
        public decimal PercDesconto { get; set; }
        public virtual Produto Produto {get;set;}
        public virtual Campanha Campanha { get; set; }
}
}
