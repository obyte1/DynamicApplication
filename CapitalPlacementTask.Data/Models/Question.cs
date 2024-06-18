using CapitalPlacementTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.Models
{
    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionTypes QuestionType { get; set; }

    }
}
