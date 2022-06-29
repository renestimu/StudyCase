using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StudyCase.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCase.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudyCaseAPIDbContext>
    {
        public StudyCaseAPIDbContext CreateDbContext(string[] args)
        {


            DbContextOptionsBuilder<StudyCaseAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
