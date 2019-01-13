using AutoMapper;
using IMS.Model.Model;
using IMS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.WebApi
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<User, LoginViewModel>();
            CreateMap<LoginViewModel, User>();
        }
    }
}
