using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IMS.Model.Model;
using IMS.IService;
using IMS.IRepository;
using IMS.Model.ViewModel;
using AutoMapper;
using System.Threading.Tasks;

namespace IMS.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        IUserRepository dal;
        IMapper Mapper;
        public UserService(IUserRepository dal, IMapper mapper)
        {
            this.dal = dal;
            base.baseDal = dal;
            Mapper = mapper;
        }

        public async Task<bool> Login(LoginViewModel lvm)
        {
           User user = Mapper.Map<User>(lvm);
           var list = await dal.Query(u => u.Account == user.Account && u.Pwd == user.Pwd);
           return list.Count() > 0;
        }


        
        //IUserRepository UserRepository;
        //public UserService(IUserRepository userRepository) : base(userRepository)
        //{
        //    UserRepository = userRepository;
        //}
    }
}
