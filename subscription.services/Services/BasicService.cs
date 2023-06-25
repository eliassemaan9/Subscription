using subscription.models.DTO.View;
using subscription.models.DTO;
using subscription.repositories.IRepositories;
using subscription.services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.services.Services
{
    public class BasicService : IBasicService
    {
        private IBasicRepository _basicRepository;
        public BasicService(IBasicRepository basicRepository)
        {
            _basicRepository = basicRepository;
        }

        public LoginResponse register(RegisterDTO registerDTO)
        {
            return _basicRepository.register(registerDTO);  
        }
    }
}
