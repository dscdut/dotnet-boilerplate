using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetBoilerplate.Domain.Payloads
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public PaginatedMeta Meta { get; set; } 
    }
}
