using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lead.ViewModels
{

    public class CreateLeadViewModel
    {
        [Required]
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
    }
}