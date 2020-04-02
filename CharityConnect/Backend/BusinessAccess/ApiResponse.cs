using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityConnect.Backend.BusinessAccess
{
    public class ApiResponse<T> 
    {
        public string token { get; set; }

        public T Data {get;set;}

    }
}
