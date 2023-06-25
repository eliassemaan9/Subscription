using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.models.DTO.View
{
    public class LoginResponse
    {
        public long userId { get; set; }

        public string accessToken { get; set; }

        public DateTime expiryDate { get; set; }
    }
}
