using Lead.Models;
using Microsoft.EntityFrameworkCore;

namespace Lead.Data
{
    public class AppDbContext : DbContext
    {
        //Contexto de dados da aplicação = representaçã do banco
        public DbSet<Leads> MeusLeads { get; set; }
        //DbSet = Representação da tabela do banco

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
        //Configuração do banco
    }
}