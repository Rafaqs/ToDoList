using Microsoft.EntityFrameworkCore;
using Todo.Dominio;

namespace Todo.Repo
{
    public class TodoContext : DbContext
    {
        public DbSet<ToDo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString: "DataSource=todo.db;Cache=Shared");
    }
}
