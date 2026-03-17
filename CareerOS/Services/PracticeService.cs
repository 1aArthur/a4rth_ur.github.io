using CareerOS.Models;
using Microsoft.Data.Sqlite;

namespace CareerOS.Services;

public class PracticeService
{
    private readonly DatabaseService _database;

    public PracticeService(DatabaseService database) => _database = database;

    public async Task AddRecordAsync(PracticeRecord record)
    {
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO PracticeRecords (Platform, Title, Theme, Difficulty, Language, TimeSpentMinutes, Status, Notes, Link, Date)
                            VALUES ($p,$t,$th,$d,$l,$time,$s,$n,$link,$date)";
        cmd.Parameters.AddWithValue("$p", record.Platform);
        cmd.Parameters.AddWithValue("$t", record.Title);
        cmd.Parameters.AddWithValue("$th", record.Theme);
        cmd.Parameters.AddWithValue("$d", record.Difficulty);
        cmd.Parameters.AddWithValue("$l", record.Language);
        cmd.Parameters.AddWithValue("$time", record.TimeSpentMinutes);
        cmd.Parameters.AddWithValue("$s", (int)record.Status);
        cmd.Parameters.AddWithValue("$n", record.Notes);
        cmd.Parameters.AddWithValue("$link", record.Link ?? string.Empty);
        cmd.Parameters.AddWithValue("$date", record.Date.ToString("O"));
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<PracticeRecord>> GetRecordsAsync()
    {
        var list = new List<PracticeRecord>();
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Platform, Title, Theme, Difficulty, Language, TimeSpentMinutes, Status, Notes, Link, Date FROM PracticeRecords ORDER BY Date DESC";
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new PracticeRecord
            {
                Id = reader.GetInt32(0), Platform = reader.GetString(1), Title = reader.GetString(2), Theme = reader.GetString(3), Difficulty = reader.GetString(4),
                Language = reader.GetString(5), TimeSpentMinutes = reader.GetInt32(6), Status = (ExerciseStatus)reader.GetInt32(7), Notes = reader.GetString(8),
                Link = reader.GetString(9), Date = DateTime.Parse(reader.GetString(10))
            });
        }
        return list;
    }
}
