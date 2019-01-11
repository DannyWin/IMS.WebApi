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
        IRoleRepository dal;
        public RoleService(IRoleRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
        //IRoleRepository RoleRepository;
        //public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        //{

        //}
    }
}
