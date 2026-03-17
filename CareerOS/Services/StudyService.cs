using CareerOS.Models;
using Microsoft.Data.Sqlite;

namespace CareerOS.Services;

public class StudyService
{
    private readonly DatabaseService _database;

    public StudyService(DatabaseService database) => _database = database;

    public async Task<List<Subject>> GetSubjectsAsync()
    {
        var list = new List<Subject>();
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Name, Description, MasteryLevel, Priority, Deadline, Status FROM Subjects ORDER BY Priority DESC";
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new Subject
            {
                Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), MasteryLevel = reader.GetInt32(3),
                Priority = (PriorityLevel)reader.GetInt32(4), Deadline = DateTime.Parse(reader.GetString(5)), Status = (StudyStatus)reader.GetInt32(6)
            });
        }
        return list;
    }

    public async Task AddSubjectAsync(Subject subject)
    {
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Subjects (Name, Description, MasteryLevel, Priority, Deadline, Status) VALUES ($n,$d,$m,$p,$dl,$s)";
        cmd.Parameters.AddWithValue("$n", subject.Name);
        cmd.Parameters.AddWithValue("$d", subject.Description);
        cmd.Parameters.AddWithValue("$m", subject.MasteryLevel);
        cmd.Parameters.AddWithValue("$p", (int)subject.Priority);
        cmd.Parameters.AddWithValue("$dl", subject.Deadline.ToString("O"));
        cmd.Parameters.AddWithValue("$s", (int)subject.Status);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteSubjectAsync(int id)
    {
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Subjects WHERE Id = $id";
        cmd.Parameters.AddWithValue("$id", id);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task AddSessionAsync(StudySession session)
    {
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO StudySessions (SubjectId, Topic, Date, DurationMinutes, SessionType, Notes, ProductivityScore) VALUES ($sid,$t,$d,$dur,$st,$n,$p)";
        cmd.Parameters.AddWithValue("$sid", session.SubjectId);
        cmd.Parameters.AddWithValue("$t", session.Topic);
        cmd.Parameters.AddWithValue("$d", session.Date.ToString("O"));
        cmd.Parameters.AddWithValue("$dur", session.DurationMinutes);
        cmd.Parameters.AddWithValue("$st", (int)session.SessionType);
        cmd.Parameters.AddWithValue("$n", session.Notes);
        cmd.Parameters.AddWithValue("$p", session.ProductivityScore);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<StudySession>> GetSessionsAsync()
    {
        var list = new List<StudySession>();
        await using var conn = new SqliteConnection(_database.ConnectionString);
        await conn.OpenAsync();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, SubjectId, Topic, Date, DurationMinutes, SessionType, Notes, ProductivityScore FROM StudySessions ORDER BY Date DESC";
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new StudySession
            {
                Id = reader.GetInt32(0), SubjectId = reader.GetInt32(1), Topic = reader.GetString(2), Date = DateTime.Parse(reader.GetString(3)),
                DurationMinutes = reader.GetInt32(4), SessionType = (SessionType)reader.GetInt32(5), Notes = reader.GetString(6), ProductivityScore = reader.GetInt32(7)
            });
        }
        return list;
    }
}
