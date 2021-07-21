using IBoxs.Excecao;
using IBoxs.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IBoxs.Model
{
    public class Loja : Pessoa
    {
        public Loja()
        {
            this.TipoPessoa = TipoPessoa.Loja;
        }
        public string Id { get; set; }
        [StringLength(100)]
        public string RazaoSocial { get; set; }
        public IntegradorPagamento IntegradorPagamento { get; set; }
        public string PublicKey { get; set; }
        public string AccessToken { get; set; }
        public decimal TaxaBoleto { get; set; }
        public string Whatsapp { get; set; }
        public string LogoMarca { get; set; }
        public string QuemSomos { get; set; }
        public string PoliticaPrivacidade { get; set; }
        public string PoliticaTrocaDevolucao { get; set; }
        public bool VerEstoque { get; set; }
        public bool PagamentoRetira { get; set; }
        public int QtdMinima { get; set; }
        public string IdFacebookPixel { get; set; }

        [MaxLength(20, ErrorMessage = "Tamanho máximo é de 20 caracteres")]
        public string MensagemCompartilhamento { get; set; }
        public string Senha { get; set; }
    }
    public enum IntegradorPagamento
    {
        MercadoPago = 1,
        PagSeguro = 2
    }

    public class Cadastro
    {

        public Cadastro()
        {

        }


        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public TipoDocumento TipoDoc { get; set; }

        public Loja ConverterCadastroLoja()
        {
            return new Loja
            {
                RazaoSocial = RazaoSocial,
                NomePessoa = NomeFantasia,
                TipoDocumento = TipoDoc,
                CNPJCPF = Documento.RemoverMascaras(),
                Email = Email,
                Ativo = true,
                Senha = Senha.Equals(ConfirmaSenha) ? Senha : string.Empty,
                Whatsapp = Celular.RemoverMascaras()
            };
        }
    }
}
