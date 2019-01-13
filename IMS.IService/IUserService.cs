using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Model.Model;
using IMS.Model.ViewModel;

namespace IMS.IService
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> Login(LoginViewModel lvm);
    }
}
