using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Classes
{
    [Serializable()]
    public class ExitWithoutSaveException : System.Exception
    {
        public ExitWithoutSaveException() : base()
        {

        }
        public ExitWithoutSaveException(string message) : base(message)
        {

        }
        public ExitWithoutSaveException(string message, System.Exception inner) : base(message, inner)
        {

        }

        // Конструктор для сериализации 
        protected ExitWithoutSaveException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
