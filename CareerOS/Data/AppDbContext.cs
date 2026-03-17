using CareerOS.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<StudySession> StudySessions => Set<StudySession>();
    public DbSet<PracticeRecord> PracticeRecords => Set<PracticeRecord>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();
}
