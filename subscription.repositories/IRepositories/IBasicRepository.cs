using subscription.models.DTO.View;
using subscription.models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.IRepositories
{
    public interface IBasicRepository
    {
        LoginResponse register(RegisterDTO registerDTO);
        LoginResponse login(LoginDTO loginDTO);
    }
}
