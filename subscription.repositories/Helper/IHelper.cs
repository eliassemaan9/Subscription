using subscription.models;
using subscription.models.Models;
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
