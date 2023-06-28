using subscription.models.DTO;
using subscription.models.DTO.View;
using subscription.models.Models;
using subscription.repositories.Helper;
using subscription.repositories.IRepositories;
using subscription.repositories.Log4net;
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
        private ILog4net _log4Net;
        private IHelper _helper;
        public BasicRepository(SubscriptionContext context, IHelper helper,ILog4net log4Net)
        {
            _context = context;
            _helper = helper;
            _log4Net = log4Net;
        }

        public LoginResponse register(RegisterDTO registerDTO) 
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!_helper.CheckIfEmail(registerDTO.email))
                    {
                        throw new UnauthorizedAccessException("Invalid Email structure");
                    }
                    var exists = _context.Users.Where(c => c.Email.ToLower() == _helper.EncryptString(registerDTO.email.ToLower()) && c.EmailConfirmed == 1 && c.IsDeleted == 0).FirstOrDefault();
                    if (exists != null)
                    {
                        throw new UnauthorizedAccessException("Email Already exists");
                    }
                    LoginResponse loginResponse = new LoginResponse();
                    User user = new User();
                    user.UserName = _helper.EncryptString(registerDTO.userName);
                    user.Email = _helper.EncryptString(registerDTO.email.ToLower());
                    user.Salt = _helper.GenerateSalt(64);
                    user.PasswordHash = _helper.HashStringHMACSHA512(registerDTO.password, user.Salt);
                    user.FirstName = _helper.EncryptString(registerDTO.firstName);
                    user.LastName = _helper.EncryptString(registerDTO.lastName);
                    user.IsDeleted = 0;
                    user.EmailConfirmed = 0;
                    user.IsActive = 1;
                    user.PhoneNumber = _helper.EncryptString(registerDTO.phoneNumber);
                    user.PhoneNumberConfirmed = 0;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    LoginDTO loginDTO = new LoginDTO();
                    loginDTO.Login = registerDTO.email;
                    loginDTO.Password = registerDTO.password;

                    loginResponse = login(loginDTO);
                    transaction.Commit();
                    return loginResponse;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public LoginResponse login(LoginDTO loginDTO)
        {

            //////////////logging/////////////
            _log4Net.Info("HELLO");
            LoginResponse loginResponse = new LoginResponse();
            var email = _helper.EncryptString(loginDTO.Login.ToLower()).ToLower();

            User user = _context.Users.Where(c => c.Email.ToLower() == email && c.IsDeleted == 0).FirstOrDefault();
            if(user == null)
            {
                throw new UnauthorizedAccessException("Invalid Email or Password");
            }
            var pass = _helper.HashStringHMACSHA512(loginDTO.Password,user.Salt);
            user = _context.Users.Where(c => c.PasswordHash == pass && c.Email.ToLower() == email).FirstOrDefault();
            if( user == null )
            {
                throw new UnauthorizedAccessException("Invalid Email or Password");
            }
            string accessToken = _helper.GenerateJwt(user);
            loginResponse.expiryDate = DateTime.UtcNow.AddHours(12);
            loginResponse.userId = user.Id;
            loginResponse.accessToken = accessToken;

            Session sessions = new Session();
            sessions.UserId = user.Id;
            sessions.AccessToken = _helper.EncryptString(accessToken);
            sessions.LoginTime = DateTime.Now;
            sessions.LogoutTime = null;
            _context.Sessions.Add(sessions);
            _context.SaveChanges();
            return loginResponse;
        }
    }
}
