using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crud.EF
{
   public class StudentContextFactory : IDesignTimeDbContextFactory<StudentDBContext>
    {
        public StudentDBContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<StudentDBContext>();
            options.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = studentcrud; Trusted_Connection = True");
            return new StudentDBContext(options.Options);
        }
    }
}
