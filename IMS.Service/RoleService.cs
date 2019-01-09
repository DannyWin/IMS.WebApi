using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IMS.Model;
using IMS.IService;
using IMS.IRepository;

namespace IMS.Service
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        IRoleRepository RoleRepository;
        public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        {

        }
    }
}
