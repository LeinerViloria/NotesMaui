using Microsoft.EntityFrameworkCore;

namespace Notes.Backend.Context;

public class NotesContext(DbContextOptions options) : DbContext(options)
{
    
}