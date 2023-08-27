using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture_Domain.Dtos.Requests
{
    public class DrugRequestDto
    {
       
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
