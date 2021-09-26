using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication_Service.Model
{
    public class DetailModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }

        public string  Address { get; set; }

        public string Token { get; set; }
    }
}
