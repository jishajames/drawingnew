using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using MyClassLibrarynew.Models;
using MyClassLibrarynew.Helpers;

namespace MyClassLibrarynew.Services
{
    public interface IUserAuthenticationService
    {
        bool GetLoginUserAuthentication(Login log,  out Login newlog);
    }
    public class UserAuthenticationService : IUserAuthenticationService
    {
        public readonly DrawingDbContext _context;
        public UserAuthenticationService(DrawingDbContext context)
        {
            _context = context;
        }
        public bool GetLoginUserAuthentication(Login log,  out Login newlog)
        {
            newlog = null;
           if (log!=null && !string.IsNullOrEmpty(log.Username) && !string.IsNullOrEmpty(log.Password))
            {
                newlog = _context.Logins.SingleOrDefault(x => x.Username == log.Username);
                if(newlog!=null  && PasswordHelper.VerifyEncryptedPassword(newlog.Password, log.Password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
