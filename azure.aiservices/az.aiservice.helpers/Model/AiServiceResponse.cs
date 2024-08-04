using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace az.aiservice.helpers.Model
{
    public class AiServiceResponse
    {
        public HttpStatusCode ResponseStatus { get; set; }  
        public string Response { get; set; }

    }
}
