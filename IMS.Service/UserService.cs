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
        IUserRepository dal;
        public UserService(IUserRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
        //IUserRepository UserRepository;
        //public UserService(IUserRepository userRepository) : base(userRepository)
        //{
        //    UserRepository = userRepository;
        //}
    }
}
