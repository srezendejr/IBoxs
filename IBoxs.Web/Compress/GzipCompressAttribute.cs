using System.Net.Http;
using System.Web.Http.Filters;

namespace IBoxs.Web.Compress
{
    public class GzipCompressAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var content = actionExecutedContext.Response?.Content;
            var bytes = content?.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            var zlibbedContent = bytes == null ? new byte[0] :
            GzipCompressionHelper.DeflateByte(bytes);
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Content = new ByteArrayContent(zlibbedContent);
                actionExecutedContext.Response.Content.Headers.Add("Content-encoding", "gzip");
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}