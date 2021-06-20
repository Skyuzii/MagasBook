using System.Collections.Generic;

namespace MagasBook.Application.Dto
{
    public class BadResponseDto
    {
        public IList<string> Errors { get; set; } = new List<string>();
    }
}