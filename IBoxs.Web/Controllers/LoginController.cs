using IBoxs.Data.Services;
using IBoxs.Web.Models;
using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace IBoxs.Web.Controllers
{
    public class LoginController : ApiController
    {
        public IIdentity _identity;
        public ILoginService _loginService;

        public LoginController(ILoginService LoginService)
        {
            _loginService = LoginService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> ValidaAutenticado()
        {
            bool isAutenticated = false;
            if (_identity.IsAuthenticated)
            {
                isAutenticated = true;
            }
            return Ok(isAutenticated);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> Autenticar(AutenticarDto model)
        {
            var fezLogin = await _loginService.VerificaLogin(model.Id, model.Password);
            if (fezLogin)
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                  1,
                                                  model.Id.ToString(),  // Id do usuário é muito importante
                                                  DateTime.Now,
                                                  DateTime.Now.AddMinutes(30),  // validade 30 min tá bom demais
                                                  false, // Se você deixar true, o cookie ficará no PC do usuário
                                                  model.Id,
                                                  model.Id);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return Ok(fezLogin);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Autenticar(string nome, string senha)
        {
            var fezLogin = await _loginService.VerificaLogin(nome, senha);
            if (fezLogin)
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                  1,
                                                  nome,  // Id do usuário é muito importante
                                                  DateTime.Now,
                                                  DateTime.Now.AddMinutes(30),  // validade 30 min tá bom demais
                                                  false, // Se você deixar true, o cookie ficará no PC do usuário
                                                  nome,
                                                  nome);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return Ok(fezLogin);
        }
    }
}
