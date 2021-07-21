using System.Runtime.Serialization;

namespace IBoxs.Excecao
{
    public class BllException:BaseException
    {
        public BllException()
        {
        }

        public BllException(string message) : base(message)
        {
        }

        public BllException(string message, string innerException) : base(message, innerException)
        {
        }

        public BllException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected BllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
