using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class ErrorsException : Exception
    {
        public ErrorsException(string? message) : base(message)
        {

        }

        public ErrorsException(string[] message) : base(string.Join(";", message))
        {

        }
    }
}
