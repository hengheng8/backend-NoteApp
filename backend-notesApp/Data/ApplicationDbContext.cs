using backend_notesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_notesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}
