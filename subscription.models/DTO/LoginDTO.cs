using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
