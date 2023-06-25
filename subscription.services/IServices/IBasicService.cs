using subscription.models.DTO;
using subscription.models.DTO.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.services.IServices
{
    public interface IBasicService
    {
        LoginResponse register(RegisterDTO registerDTO);
    }
}
