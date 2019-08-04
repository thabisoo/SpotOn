using SpotOn.ApplicationLogic.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> RegisterAsync(RegisterUserEntity user);

        UserEntity LogInAsync(LogInEntity user);
    }
}
