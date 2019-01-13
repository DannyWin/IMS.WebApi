using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.IService;
using IMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : BaseController<Org>
    {
        private IOrgService OrgService;
        public OrgController(IOrgService orgService) : base(orgService)
        {
        }
    }
}