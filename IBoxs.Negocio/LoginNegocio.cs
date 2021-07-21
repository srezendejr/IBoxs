using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IBoxs.Data.Services;
using IBoxs.Model;
using System.Linq;
using IBoxs.Excecao;
using System.Globalization;
using IBoxs.Util;
using IBoxs.Data;
using System.Data.Entity;
using IBoxs.Data.Context;

namespace IBoxs.Negocio
{
    public class LoginNegocio : ILoginService
    {
        private Context _context;

        public LoginNegocio(Context Context)
        {
            _context = Context;
        }

        public async Task<bool> VerificaLogin(string id, string password)
        {
            bool fezLogin = true;
            string doc = id.Trim().RemoverMascaras();
            var loja = await _context.Lojas.FirstOrDefaultAsync(f => (f.CNPJCPF == doc || f.Email == id) && f.Senha == password);
            if (loja == null)
            {
                fezLogin = false;
                throw new BllException("Usuário ou senha incorretos");
            }

            //string sql = $"SELECT CodigoEmpresaControladora FROM EmpresaControladora WHERE LTRIM(RTRIM(CgcEmpresaControladora)) = '{id.Trim().RemoverMascaras()}' AND LTRIM(RTRIM(SenhaPerfil)) = '{password.Trim()}'";
            //var dtLoja = await _conexaoAccess.RetornaDataTable(sql, loja.CaminhoBancoDados);
            //if (dtLoja.Rows.Count > 0)
            //    fezLogin = true;

            return fezLogin;
        }

        public static bool VerificarLogin(string UserName, string PassWord)
        {
            bool fezLogin = true;
            using (Context context = new Context())
            {
                fezLogin = context.Lojas.Any(user => (user.CNPJCPF == UserName.RemoverMascaras() || user.Email == UserName) && user.Senha == PassWord);
            }
            return fezLogin;
        }
    }
}
