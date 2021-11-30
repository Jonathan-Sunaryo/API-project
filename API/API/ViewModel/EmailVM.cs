using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class EmailVM
    {
        [Required]
        public string Email { get; set; }
    }
}
