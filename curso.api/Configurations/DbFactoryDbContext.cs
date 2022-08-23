using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<CursoDbContext>();
            options.UseSqlServer("Data Source=DESKTOP-MT5OVJG\\SQLEXPRESS;Initial Catalog=CURSO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            CursoDbContext contexto = new CursoDbContext(options.Options);

            return contexto;
        }

    }
}
