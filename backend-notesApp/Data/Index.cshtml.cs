using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using backend_notesApp.Models;

namespace backend_notesApp.Data
{
    public class IndexModel : PageModel
    {
        private readonly backend_notesApp.Data.backend_notesAppContext _context;

        public IndexModel(backend_notesApp.Data.backend_notesAppContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Note = await _context.Note.ToListAsync();
        }
    }
}
