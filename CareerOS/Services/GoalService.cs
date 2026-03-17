using CareerOS.Data;
using CareerOS.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Services;

public interface IGoalService
{
    Task<List<Goal>> GetAsync();
    Task AddAsync(Goal goal);
}

public class GoalService : IGoalService
{
    private readonly AppDbContext _db;
    public GoalService(AppDbContext db) => _db = db;

    public Task<List<Goal>> GetAsync() => _db.Goals.OrderBy(x => x.Periodo).ToListAsync();
    public async Task AddAsync(Goal goal)
    {
        _db.Goals.Add(goal);
        await _db.SaveChangesAsync();
    }
}
