using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Webstore.Domain.Entity;
using Webstore.Domain.Response;
using Webstore.Domain.ViewModel.User;

namespace Webstore.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<IBaseResponse<User>> UpdateUser(Guid id, User model);
        Task<IBaseResponse<User>> GetUserByEmail(string email);
    }
}
