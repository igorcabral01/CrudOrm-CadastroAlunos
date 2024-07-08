using Crudmvc.Estudantes;
using Microsoft.EntityFrameworkCore;
namespace Crudmvc.Data;
//1 Microsoft.EntityFrameworkCore
// Pomelo.EntityFrameworkCore.MySql
// Microsoft.EntityFrameworkCore.Tools
//terminal (dd-Migration CriandoTabela -> Update-Database )<-- Console Nuget
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts)
    {
        
    }
    public DbSet<Estudante> Estudantes {  get; set; }

}
//Logica para implantação do banco de dados sqlserve Workbench