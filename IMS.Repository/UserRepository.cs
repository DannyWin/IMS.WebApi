using System;
using System.Collections.Generic;
using System.Text;
using IMS.Model.Model;
using IMS.IRepository;

namespace IMS.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
    }
}
