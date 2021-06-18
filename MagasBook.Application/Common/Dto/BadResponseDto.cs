using System.Collections;
using System.Collections.Generic;

namespace MagasBook.Application.Common.Dto
{
    public class BadResponseDto
    {
        public IList<string> Errors { get; set; } = new List<string>();
    }
}