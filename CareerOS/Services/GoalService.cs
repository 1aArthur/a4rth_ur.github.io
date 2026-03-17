using CareerOS.Models;
using Microsoft.Data.Sqlite;

namespace CareerOS.Services;

public class GoalService
{
    private readonly DatabaseService _database;

    public GoalService(DatabaseService database) => _database = database;

    public async Task<List<Goal>> GetGoalsAsync()
    {
        var list = new List<Goal>();
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Name, Period, TargetValue, CurrentValue FROM Goals";
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new Goal
            {
                Id = reader.GetInt32(0), Name = reader.GetString(1), Period = reader.GetString(2), TargetValue = reader.GetInt32(3), CurrentValue = reader.GetInt32(4)
            });
        }
        return list;
    }

    public async Task AddGoalAsync(Goal goal)
    {
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Goals (Name, Period, TargetValue, CurrentValue) VALUES ($n,$p,$t,$c)";
        cmd.Parameters.AddWithValue("$n", goal.Name);
        cmd.Parameters.AddWithValue("$p", goal.Period);
        cmd.Parameters.AddWithValue("$t", goal.TargetValue);
        cmd.Parameters.AddWithValue("$c", goal.CurrentValue);
        await cmd.ExecuteNonQueryAsync();
    }
}
