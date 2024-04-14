
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Backend.Context;
using Notes.Entities;

namespace Notes.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController(IDbContextFactory<NotesContext> contextFactory) : ControllerBase
{
    private readonly IDbContextFactory<NotesContext> ContextFactory = contextFactory;

    [HttpGet]
    public IEnumerable<Note> Get()
    {
        using var context = ContextFactory.CreateDbContext();

        var Data = context.Set<Note>()
            .AsNoTracking()
            .ToList();

        return Data;
    }

    [HttpGet("{name}")]
    public IEnumerable<Note> Get(string Name)
    {
        using var context = ContextFactory.CreateDbContext();

        var Data = context.Set<Note>()
            .AsNoTracking()
            .Where(x => x.Name.Contains(Name))
            .ToList();

        return Data;
    }

    [HttpPost]
    public Note Create([FromBody] Note note)
    {
        using var context = ContextFactory.CreateDbContext();

        note.CreationDate = DateTime.UtcNow;

        context.Add(note);
        context.SaveChanges();

        return note;
    }

    [HttpPut]
    public bool UpdateName([FromBody] Note NoteToUpdate)
    {
        using var context = ContextFactory.CreateDbContext();

        var Result = context.Set<Note>()
            .Where(x => x.Rowid == NoteToUpdate.Rowid)
            .ExecuteUpdate(x => 
                x.SetProperty(y => y.Name, NoteToUpdate.Name)
             );

        return Result == 1;
    }

    [HttpDelete("{rowid}")]
    public bool Delete(int rowid)
    {
        using var context = ContextFactory.CreateDbContext();

        var Result = context.Set<Note>()
            .Where(x => x.Rowid == rowid)
            .ExecuteDelete();

        return Result == 1;
    }

}