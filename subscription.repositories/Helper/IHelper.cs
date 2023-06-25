using subscription.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.Helper
{
    public interface IHelper
    {
        string GenerateJwt(User user);
    }
}
