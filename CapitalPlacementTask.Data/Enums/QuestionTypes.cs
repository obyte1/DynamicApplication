using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Data.Enums
{
    public enum QuestionTypes
    {
        [Display(Name = "Paragraph")]
        Paragraph = 1,

        [Display(Name = "Yes or No")]
        YesOrNo = 2,

        [Display(Name = "Dropdown")]
        Dropdown = 3,

        [Display(Name = "Date")]
        Date = 4,

        [Display(Name = "Number")]
        Number = 5
    }
}
