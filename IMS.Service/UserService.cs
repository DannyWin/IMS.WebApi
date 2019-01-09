using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IMS.Model;
using IMS.IService;
using IMS.IRepository;

namespace IMS.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        IUserRepository UserRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {

        }
    }
}
