using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IMS.Model;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace IMS.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }


        public DbSet<User> User { set; get; }
        public DbSet<Org> Org { set; get; }
        public DbSet<Role> Role { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
        }

    }
}
