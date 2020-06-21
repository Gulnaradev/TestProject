using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsDAO
{
    public class MyDBContext: DbContext
    {
        public MyDBContext() : base("MyConnectionString")
        {

        }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
