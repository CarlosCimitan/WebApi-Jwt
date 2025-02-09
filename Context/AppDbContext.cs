using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WepApiProjetoReact.Models;

namespace WepApiProjetoReact.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {    
        }

        public DbSet<AlunoModel> Alunos { get; set; }
    }
}
