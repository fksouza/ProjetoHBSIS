using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoHBSIS.Models
{
    public class ProjetoHBSISContext : DbContext
    {
        public ProjetoHBSISContext(DbContextOptions<ProjetoHBSISContext> options)
            : base(options)
        {

        }

        public DbSet<Contribuinte> Contribuinte { get; set; }
        public DbSet<SalarioMinimo> SalarioMinimo { get; set; }
        public DbSet<ImpostodeRenda> ImpostodeRenda { get; set; }
    }
}
