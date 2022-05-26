using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Context
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
        }
        public DbSet<ToDoList>? toDoLists { get; set; }

    }
}
