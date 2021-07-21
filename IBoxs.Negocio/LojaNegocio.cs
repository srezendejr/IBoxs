using IBoxs.Data.Context;
using IBoxs.Data.Services;
using IBoxs.Excecao;
using IBoxs.Model;
using IBoxs.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBoxs.Negocio
{
    public class LojaNegocio : ILojaService
    {
        private Context _context;

        public LojaNegocio(Context Context)
        {
            _context = Context;
        }
        public Task AlterarLOja(Loja Loja)
        {
            throw new NotImplementedException();
        }

        public Task<Loja> BuscarLojaPorCNPJCPF(string cNPJCPF)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirLoja(Loja Loja)
        {
            throw new NotImplementedException();
        }

        public async Task IncluirLoja(Loja Loja)
        {
            if (Loja.TipoDocumento == TipoDocumento.CNPJ && string.IsNullOrEmpty(Loja.RazaoSocial))
                throw new BllException("Informe a razão social");
            if (string.IsNullOrEmpty(Loja.NomePessoa))
                throw new BllException("Informe o nome");
            if (Loja.TipoDocumento == TipoDocumento.CNPJ && !Loja.CNPJCPF.IsCnpj())
                throw new BllException("Informe um CNPJ válido");
            if (Loja.TipoDocumento == TipoDocumento.CPF && !Loja.CNPJCPF.IsCpf())
                throw new BllException("Informe um CPF válido");
            if (string.IsNullOrEmpty(Loja.Email) && !Loja.Email.IsEmail())
                throw new BllException("Informe um Email válido");
            if (string.IsNullOrEmpty(Loja.Whatsapp))
                throw new BllException("Informe o número de telefone válido");
            if (string.IsNullOrEmpty(Loja.Senha))
                throw new BllException("Informe uma senha");

            _context.Salvar<Loja>(Loja);
            await _context.Commit();
        }

        public Task<List<Loja>> ListarLojas()
        {
            throw new NotImplementedException();
        }

        public Task<Loja> PesquisarLojaPorCodigo(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Loja> PesquisarPorIdLoja(int IdLoja)
        {
            throw new NotImplementedException();
        }
    }
}
