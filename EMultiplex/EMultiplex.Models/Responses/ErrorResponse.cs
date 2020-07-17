using System.Collections.Generic;

namespace EMultiplex.Models.Responses
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
