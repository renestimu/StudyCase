using Microsoft.EntityFrameworkCore;
using StudyCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCase.Persistence.Contexts
{
    public class StudyCaseAPIDbContext : DbContext
    {
        public StudyCaseAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }    
    }
}
