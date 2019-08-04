using AutoMapper;
using SpotOn.ApplicationLogic.Entities.Users;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.Domain;
using SpotOn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User> userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<UserEntity> RegisterAsync(RegisterUserEntity registerUserEntity)
        {
            var user = _mapper.Map<User>(registerUserEntity);
            user.CreatedAt = DateTimeOffset.Now;
            user.UpdatedAt = DateTimeOffset.Now;

            _userRepository.Add(user);
            await _userRepository.SaveAsync();

            return _mapper.Map<UserEntity>(user);
        }

        public UserEntity LogInAsync(LogInEntity logInEntity)
        {
            var user = _userRepository.Where(u => u.Email == logInEntity.Email 
                            && u.Password == logInEntity.Password)
                            .FirstOrDefault();

            if (user == null)
                return null;

            return _mapper.Map<UserEntity>(user);
        }
    }
}
