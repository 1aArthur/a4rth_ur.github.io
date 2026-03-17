using CareerOS.Data;
using CareerOS.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Services;

public interface IPracticeService
{
    Task<List<PracticeRecord>> GetAsync();
    Task AddAsync(PracticeRecord record);
}

public class PracticeService : IPracticeService
{
    private readonly AppDbContext _db;
    public PracticeService(AppDbContext db) => _db = db;

    public Task<List<PracticeRecord>> GetAsync() => _db.PracticeRecords.OrderByDescending(x => x.Id).ToListAsync();

    public async Task AddAsync(PracticeRecord record)
    {
        _db.PracticeRecords.Add(record);
        await _db.SaveChangesAsync();
    }
}
