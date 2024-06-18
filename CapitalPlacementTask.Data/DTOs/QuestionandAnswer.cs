using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.DTOs
{
    public class QuestionandAnswer
    {
        public int QuestionId { get; set; }
        public required string Answer { get; set; }
    }
}
