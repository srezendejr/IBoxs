using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace IBoxs.Util
{
    public static class Email
    {
        public static void EnviarEmail(string destinatario, string nomeLoja, string nomeCliente, Dictionary<string, int> lstProdutos, string numeroPedido, string endereco, string formaPagamento, string assunto)
        {
            string emailRemetente = "iboxs@fabrisistemas.com.br";
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Olá {nomeCliente} <br/> <br/>")
                .AppendLine($"IBOXS - Seu pedido <b>{numeroPedido}</b> foi aprovado! <br/> <br/>")
                .AppendLine($"A {nomeLoja} agradece sua compra! <br/> <br/>")
                .AppendLine($"Abaixo, listamos os produtos do seu pedido: {numeroPedido}<br/><br/>");
            foreach (var item in lstProdutos)
            {
                sb.AppendLine($"Item: {item.Key.PadLeft(20, ' ')}  Qtd: {item.Value} <br/>");
            }
            sb.AppendLine($"<br/> <b>Endereço de Entrega:</b> {endereco} <br/>")
                .AppendLine($"<br/><b>Forma de pagamento: </b> {formaPagamento} <br/>");
            sb.AppendLine("<br/> Em breve enviaremos seu pedido e código de rastreamento para você acompanhar sua compra! <br/>")
                .AppendLine("Ah, se tiver qualquer dúvida, entre em contato com a gente.<br/> <br/>")
                .AppendLine("Um abraço, <br/> <br/>")
                .AppendLine($"Equipe - {nomeLoja}");
            string mensagem = sb.ToString();
            var mail = new MailMessage(emailRemetente, destinatario, assunto, mensagem);
            mail.ReplyToList.Add(emailRemetente);
            mail.IsBodyHtml = true;
            var smtpClient = new SmtpClient("email-ssl.com.br", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailRemetente, "Aez4659@dz");
            smtpClient.Send(mail);

        }

        public static void EnviarEmail(string destinatario, string assunto, string mensagem)
        {
            string emailRemetente = "iboxs@fabrisistemas.com.br";
            StringBuilder sb = new StringBuilder();
            var mail = new MailMessage(emailRemetente, destinatario, assunto, mensagem);
            mail.ReplyToList.Add(emailRemetente);
            mail.IsBodyHtml = true;
            var smtpClient = new SmtpClient("email-ssl.com.br", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailRemetente, "Aez4659@dz");
            smtpClient.Send(mail);

        }
    }
}
