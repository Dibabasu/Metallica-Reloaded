using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Common.Exceptions
{
    public class DuplicateFoundException : Exception
    {
        public DuplicateFoundException()
            : base()
        {
        }

        public DuplicateFoundException(string message)
            : base(message)
        {
        }

        public DuplicateFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DuplicateFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists.")
        {
        }
    }
}
