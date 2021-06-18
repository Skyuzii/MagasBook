using System;

namespace MagasBook.Application.Common.Exceptions
{
    public class RestException : Exception
    {
        public int StatusCode { get; set; }

        public RestException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public RestException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}