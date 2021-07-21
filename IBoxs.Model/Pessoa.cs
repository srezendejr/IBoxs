using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Model
{
    public abstract class Pessoa
    {
        public int IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string CNPJCPF { get; set; }
        public string IERG { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }


    }

    public enum TipoPessoa
    {
        Loja = 0,
        Cliente = 1
    }
    public enum TipoDocumento
    {
        CPF = 0,
        CNPJ = 1
    }

}
