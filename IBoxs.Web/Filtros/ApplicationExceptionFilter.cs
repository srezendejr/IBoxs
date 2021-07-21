using IBoxs.Excecao;
using IBoxs.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using WebGrease.Css.Extensions;

namespace IBoxs.Web.Filtros
{
    public class ApplicationExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            if (actionExecutedContext.Exception is BaseException)
            {
                var exception = actionExecutedContext.Exception as BaseException;

                HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                if (actionExecutedContext.Exception is WebValidationException)
                    statusCode = HttpStatusCode.NotAcceptable;

                //Dispara o evento
                List<string> mensagens = exception.GetAllInnerMessages();

                var httpresponseMessage = new HttpResponseMessage(statusCode)
                {
                    Content = new ObjectContent<ResponseMessage>(new ResponseMessage { Messages = mensagens }, new JsonMediaTypeFormatter()),
                    ReasonPhrase = exception.GetType().Name
                };

                throw new HttpResponseException(httpresponseMessage);
            }
            base.OnException(actionExecutedContext);

            /************************************************************************************************************************************
            * 417: ExpectationFailed - Erros não tratados pela aplicação.                                                                       *
            *      Erros críticos que não serão exibidos aos usuários. Estas mensagens precisam ser logadas ou encaminhadas por email à equipe. *
            *************************************************************************************************************************************/

            //Envia um email com todas as mensagens da pilha de exceção
            List<string> sb = new List<string>();
            for (Exception eCurrent = actionExecutedContext.Exception; eCurrent != null; eCurrent = eCurrent.InnerException)
                sb.Add(eCurrent.Message);


            List<string> erroAmigavel = new List<string> { "Erro amigável" };
#if DEBUG
            //Responde para o usuário com uma mensagem amigável.
            erroAmigavel.AddRange(sb);
#endif

            sb.Add("<br/><strong>StackTrace</strong><br /><br />");

            var stackSplit = actionExecutedContext.Exception.StackTrace.Split(new[] { " at " }, StringSplitOptions.None);

            stackSplit.ForEach(s => sb.Add(s + "<br /><br />"));

            sb.Add(actionExecutedContext.Exception.StackTrace);

            sb.Add("<br/><strong>AbsoluteUri</strong><br /><br />");

            sb.Add(actionExecutedContext.Request.RequestUri.AbsoluteUri);

            try
            {
                sb.Add("<br/><strong>Request</strong><br />");

                sb.Add(actionExecutedContext.Request.Headers.From);
                sb.Add("<br/>");
                sb.Add(actionExecutedContext.Request.Headers.Host);
                sb.Add("<br/>");
                sb.Add(actionExecutedContext.Request.Headers.Accept.ToString());
                sb.Add("<br/>");
                sb.Add(actionExecutedContext.Request.Headers.Date.ToString());
                sb.Add("<br/>");
                sb.Add(actionExecutedContext.Request.Headers.UserAgent.ToString());
                sb.Add("<br/>");

                sb.Add("<br/><strong>Headers</strong><br />");
                actionExecutedContext.Request.Headers.ForEach(f =>
                {
                    var values = "";
                    f.Value.ForEach(s => values += " " + s.ToString());
                    sb.Add(f.Key + ": " + values);
                });
                sb.Add("<br/><strong>---Headers----</strong><br />");
            }
            catch
            {
                //discard
            }

            try
            {
                var requestContent = actionExecutedContext.Request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                sb.Add("<br/><strong>Content</strong><br />");
                sb.Add(requestContent);
                sb.Add("<br/><strong>---Content----</strong><br />");
            }
            catch
            {
                //discard
            }

            try
            {
                sb.Add("<br/><strong>Content</strong><br />");
                using (var stream = actionExecutedContext.Request.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                {
                    if (stream.CanSeek)
                    {
                        stream.Position = 0;
                    }
                    sb.Add(actionExecutedContext.Request.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

                sb.Add("<br/><strong>---Content----</strong><br />");
            }
            catch
            {
                //discard
            }


            //Dispara o evento
            if (!(actionExecutedContext.Exception is OperationCanceledException))
            {

                string subject = "Erro Não Tratado na API [" + actionExecutedContext.Request.RequestUri.AbsoluteUri + "]";

                var body = "Ocorreu o seguinte erro na API:" + @"<br /><br />" + string.Join(", ", sb.ToArray());
                Util.Email.EnviarEmail("sergio.rezende@outlook.com", subject, body);
                Util.Email.EnviarEmail("fabri.marcos@icloud.com", subject, body);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.ExpectationFailed)
            {
                Content = new ObjectContent<ResponseMessage>(new ResponseMessage { Messages = erroAmigavel }, new JsonMediaTypeFormatter()),
                ReasonPhrase = actionExecutedContext.Exception.GetType().Name
            });            

        }
    }
}