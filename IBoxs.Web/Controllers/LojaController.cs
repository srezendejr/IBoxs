using IBoxs.Data.Services;
using IBoxs.Excecao;
using IBoxs.Model;
using IBoxs.Util;
using IBoxs.Web.Compress;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IBoxs.Web.Controllers
{
    public class LojaController : ApiController
    {
        private ILojaService _lojaService;
        public LojaController(ILojaService lojaService)
        {
            _lojaService = lojaService;
        }

        [HttpGet, GzipCompressAttribute]
        public async Task<IHttpActionResult> BuscarLoja(string IdLoja)
        {
            var loja = await _lojaService.PesquisarLojaPorCodigo(IdLoja);
            
            return Ok(loja);
        }

        [HttpGet, GzipCompressAttribute]
        public async Task<IHttpActionResult> ListarLojas()
        {
            var lstListas = await _lojaService.ListarLojas();
            return Ok(lstListas);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Salvar(Loja model)
        {
            model.CEP = model.CEP.RemoverMascaras();
            model.CNPJCPF = model.CNPJCPF.RemoverMascaras();
            model.Id = model.Id.ToLower().Trim().RemoverMascaras();
            model.Whatsapp = model.Whatsapp.RemoverMascaras();
            await _lojaService.IncluirLoja(model);
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> UploadLogo()
        {
            HttpRequestMessage request = this.Request;
            string extensao = string.Empty;
            if (!request.Content.IsMimeMultipartContent())
                throw new BllException("Tipo não permitido");

            string root = HttpContext.Current.Server.MapPath("~/image");
            var provider = new MultipartFormDataStreamProvider(root);
            var nomeCompleto = HttpContext.Current.Request.Params["descricao"].Replace("\\", string.Empty);

            if (nomeCompleto.IndexOf('.') < 0)
                extensao = "jpeg";
            else
                extensao = nomeCompleto.Split('.').Last();

            var task = await request.Content.ReadAsMultipartAsync(provider).ContinueWith<string>(o =>
            {
                string nomeArquivo = $"{Guid.NewGuid().ToString()}.{extensao}";
                string mover = $"{root}\\{nomeArquivo}";
                File.Move(provider.FileData.FirstOrDefault().LocalFileName, mover);
                return nomeArquivo;
            });

            return Ok(task);
        }

        [HttpGet, GzipCompressAttribute]
        public async Task<IHttpActionResult> BuscarLojaPorCNPJCPF(string CNPJCPF)
        {
            var loja = await _lojaService.BuscarLojaPorCNPJCPF(CNPJCPF);

            return Ok(loja);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> ExcluirLoja(string Id)
        {
            var loja = await _lojaService.PesquisarLojaPorCodigo(Id);
            await _lojaService.ExcluirLoja(loja);
            return Ok();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> Cadastro(Cadastro Model)
        {
            await _lojaService.IncluirLoja(Model.ConverterCadastroLoja());
            return Ok();
        }
    }
}
