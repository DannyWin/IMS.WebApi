﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IMS.Model;
using IMS.IService;
using IMS.IRepository;

namespace IMS.Service
{
    public class OrgService : BaseService<Org>, IOrgService
    {
        IOrgRepository OrgRepository;
        public OrgService(IOrgRepository orgRepository) : base(orgRepository)
        {

        }
    }
}