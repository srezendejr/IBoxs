using System.Runtime.Serialization;

namespace IBoxs.Excecao
{
    public class DalException:BaseException
    {
        public DalException()
        {
        }

        public DalException(string message) : base(message)
        {
        }

        public DalException(string message, string innerException) : base(message, innerException)
        {
        }

        public DalException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected DalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
