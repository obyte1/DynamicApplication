using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.DTOs
{
    public class ResponseDto
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public object? Data { get; set; }

    }
}
