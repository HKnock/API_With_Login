using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace API_With_Login.Models
{
    public class NumbersContext : DbContext
    {
        public DbSet<Numbers> Numbers { get; set; }
        public NumbersContext(DbContextOptions<NumbersContext> options) : base(options)
        {

        }
    }
}
