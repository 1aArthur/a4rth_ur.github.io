using CareerOS.Data;
using CareerOS.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Services;

public interface IStudyService
{
    Task<List<Subject>> GetSubjectsAsync();
    Task AddSubjectAsync(Subject subject);
    Task<List<StudySession>> GetSessionsAsync();
    Task AddSessionAsync(StudySession session);
}

public class StudyService : IStudyService
{
    private readonly AppDbContext _db;
    public StudyService(AppDbContext db) => _db = db;

    public Task<List<Subject>> GetSubjectsAsync() => _db.Subjects.OrderByDescending(x => x.Prioridade).ToListAsync();
    public async Task AddSubjectAsync(Subject subject)
    {
        _db.Subjects.Add(subject);
        await _db.SaveChangesAsync();
    }

    public Task<List<StudySession>> GetSessionsAsync() => _db.StudySessions.OrderByDescending(x => x.Data).ToListAsync();
    public async Task AddSessionAsync(StudySession session)
    {
        _db.StudySessions.Add(session);
        await _db.SaveChangesAsync();
    }
}
