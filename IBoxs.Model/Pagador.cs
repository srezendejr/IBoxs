using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IBoxs.Model
{
    public class Pagador
    {
        public TipoEntidade? TipoEntidade { get; set; }
        public TipoPagador? Type { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public Identificacao? Identificacao { get; set; }
        public Telefone? Phone { get; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Endereco? Endereco { get; set; }
        [NotMapped]
        public string Whatsapp { get; set; }
        public string NomeCompleto { get { return string.Format("{0} {1}", PrimeiroNome, UltimoNome); } set { throw new InvalidOperationException(); } }
    }

    public enum TipoEntidade
    {
        Individual = 0,
        Associacao = 1
    }

    /// <summary>
    /// Informa se o pagador já é cadastrado no mercado. Informar Visitante para clientes não cadastrados no mercado pago.
    /// </summary>
    public enum TipoPagador
    {
        Cliente = 0,
        Registrado = 1,
        Visitante = 2,
        Anonimo = 3
    }

    public struct Telefone
    {
        public string CodigoArea { get; set; }
        public string Numero { get; set; }
        public string Ramal { get; }
    }

    public struct Endereco
    {
        public string Logradouro { get; set; }
        /// <summary>
        /// Caso o endereço seja S/N (sem numereção) deixe como zero (0)
        /// </summary>
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public string Ibge { get; set; }

        public string LogradouroComNumero { get { return string.Format("{0}, {1}", Logradouro, Numero); } set { throw new InvalidOperationException(); } }

        public override string ToString()
        {
            return $"CEP: {CEP}, {Logradouro}, {Numero}, {(string.IsNullOrEmpty(Complemento) ? string.Empty : $" complemento: {Complemento}")}" +
                $", {Bairro} - {Cidade} - {Estado}";
        }
    }
}
