using System;
using System.Collections.Generic;
using System.Text;

namespace EMultiplex.DAL
{
    public class ErrorResponseModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
