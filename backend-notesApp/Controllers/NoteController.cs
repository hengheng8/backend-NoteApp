using backend_notesApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NoteController> _logger;

    public NoteController(ApplicationDbContext context, ILogger<NoteController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/note
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteClass>>> GetNotes()
    {
        return await _context.Notes.ToListAsync();
    }

    // GET: api/note/5
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteClass>> GetNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        return note;
    }

    // POST: api/note
    [HttpPost]
    public async Task<ActionResult<NoteClass>> CreateNote(NoteClass note)
    {
        if (note == null || string.IsNullOrEmpty(note.Title) || string.IsNullOrEmpty(note.Content))
        {
            return BadRequest("Note must have a title and content.");
        }

        _context.Notes.Add(note);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a note.");
            return StatusCode(500, "Internal server error.");
        }

        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    // PUT: api/note/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, NoteClass note)
    {
        if (id != note.Id)
        {
            return BadRequest();
        }

        _context.Entry(note).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NoteExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a note.");
            return StatusCode(500, "Internal server error.");
        }

        return NoContent();
    }

    // DELETE: api/note/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }

        _context.Notes.Remove(note);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a note.");
            return StatusCode(500, "Internal server error.");
        }

        return NoContent();
    }

    private bool NoteExists(int id)
    {
        return _context.Notes.Any(e => e.Id == id);
    }
}
