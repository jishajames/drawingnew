using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using drawingnew.Models;
using MyClassLibrarynew.Helpers;

namespace drawingnew.Services
{
    public interface IUserAuthenticationService
    {
        bool GetLoginUserAuthentication(Login log,  out Login newlog);
    }
    public class UserAuthenticationService : IUserAuthenticationService
    {
        public readonly TestContext _context;
        public UserAuthenticationService(TestContext context)
        {
            _context = context;
        }
        public bool GetLoginUserAuthentication(Login log,  out Login newlog)
        {
            newlog = null;
           if (log!=null && !string.IsNullOrEmpty(log.Username) && !string.IsNullOrEmpty(log.Password))
            {
                newlog = _context.Logins.SingleOrDefault(x => x.Username == log.Username && x.Password==log.Password);
                // if(newlog!=null  && PasswordHelper.VerifyEncryptedPassword(newlog.Password, log.Password))
                if (newlog != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
