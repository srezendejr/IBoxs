using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Model
{
    public class Cliente : Pessoa
    {
        public Cliente()
        {
            this.TipoPessoa = TipoPessoa.Cliente;
        }
        public bool BloqueadaCompra { get; set; }
        [NotMapped]
        public string LogradouroComNumero { get { return string.Format("{0}, {1}", Logradouro, Numero); } set { throw new InvalidOperationException(); } }

        public override string ToString()
        {
            return $"CEP: {CEP}, {Logradouro}, {Numero}, {(string.IsNullOrEmpty(Complemento) ? string.Empty : $" complemento: {Complemento}")}" +
                $", {Bairro} - {Cidade} - {Estado}";
        }
    }



}
