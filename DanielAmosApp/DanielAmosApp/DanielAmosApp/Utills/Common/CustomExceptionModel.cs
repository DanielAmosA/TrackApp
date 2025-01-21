using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Utills.Common
{

    /// <summary>
    /// The class responsible for Custom error representation.
    /// </summary>

    public class CustomException : Exception
    {
        public string errorCode { get; }

        public CustomException(string message, string errorCode) : base(message)
        {
            this.errorCode = errorCode;
        }
    }
}
