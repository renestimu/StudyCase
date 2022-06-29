using StudyCase.Application.Repositories;
using StudyCase.Domain.Entities;
using StudyCase.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCase.Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(StudyCaseAPIDbContext context) : base(context)
        {
        }
    }
}
