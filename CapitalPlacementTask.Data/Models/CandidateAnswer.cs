using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.Models
{
    public class CandidateAnswer : BaseEntity
    {
        public int Id { get; set; }
        public required string CandidateId { get; set; }
        public int QuestionId { get; set; }
        public required string Response { get; set; }
    }
}
