using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend_notesApp.Models;

namespace backend_notesApp.Data
{
    public class backend_notesAppContext : DbContext
    {
        public backend_notesAppContext (DbContextOptions<backend_notesAppContext> options)
            : base(options)
        {
        }

        public DbSet<backend_notesApp.Models.Note> Note { get; set; } = default!;
    }
}
