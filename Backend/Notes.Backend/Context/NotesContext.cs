using Microsoft.EntityFrameworkCore;
using Notes.Entities;

namespace Notes.Backend.Context;

public class NotesContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }
}