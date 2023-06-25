using subscription.models.DTO;
using subscription.models.DTO.View;
using subscription.models.Models;
using subscription.repositories.Helper;
using subscription.repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.Repositories
{
    public class BasicRepository : IBasicRepository
    {
        private SubscriptionContext _context;

        private IHelper _helper;
        public BasicRepository(SubscriptionContext context, IHelper helper)
        {
            _context = context;
            _helper = helper;
        }

        public LoginResponse register(RegisterDTO registerDTO) 
        {
            LoginResponse loginResponse = new LoginResponse();
            User user = new User();
            user.UserName = registerDTO.userName;
            user.Email = registerDTO.email.ToLower();
            user.Salt = _helper.GenerateSalt(64);
            user.PasswordHash = _helper.HashStringHMACSHA512(registerDTO.password, user.Salt);
            user.FirstName = registerDTO.firstName;
            user.LastName = registerDTO.lastName;
             user.IsDeleted = 0;
            user.EmailConfirmed = 0;
            user.IsActive = 1;
            user.PhoneNumber = registerDTO.phoneNumber;
            user.PhoneNumberConfirmed = 0;
            _context.Users.Add(user);
            _context.SaveChanges();

            string accessToken = "bearer" + _helper.GenerateJwt(user);
            loginResponse.expiryDate = DateTime.UtcNow.AddHours(12);
            loginResponse.userId = user.Id;
            loginResponse.accessToken = accessToken;
            
            return loginResponse;
        }
    }
}
