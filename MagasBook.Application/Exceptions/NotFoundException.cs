using System;
using Microsoft.AspNetCore.Http;

namespace MagasBook.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public int? StatusCode => StatusCodes.Status404NotFound;
    }
}