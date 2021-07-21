using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IBoxs.Excecao
{
    public class BaseException : System.Exception
    {
        protected BaseException()
        {
        }

        protected BaseException(string message)
            : base(message)
        {
        }

        protected BaseException(string message, string innerException)
            : base(message, new System.Exception(innerException))
        {
        }

        protected BaseException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public virtual List<string> GetAllInnerMessages()
        {
            List<string> lista = new List<string>();
            for (System.Exception eCurrent = this; eCurrent != null; eCurrent = eCurrent.InnerException)
            {
                if (!(eCurrent is BaseException)) continue;

                var msgs = eCurrent.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                lista.AddRange(msgs);
            }

            return lista;
        }
    }
}