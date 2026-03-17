using Microsoft.Data.Sqlite;

namespace CareerOS.Services;

public class DatabaseService
{
    private readonly string _dbPath;

    public DatabaseService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var folder = Path.Combine(appData, "CareerOS");
        Directory.CreateDirectory(folder);
        _dbPath = Path.Combine(folder, "careeros.db");
    }

    public string ConnectionString => $"Data Source={_dbPath}";

    public async Task InitializeAsync()
    {
        await using var connection = new SqliteConnection(ConnectionString);
        await connection.OpenAsync();

        var commands = new[]
        {
            @"CREATE TABLE IF NOT EXISTS Subjects (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Description TEXT, MasteryLevel INTEGER, Priority INTEGER, Deadline TEXT, Status INTEGER)",
            @"CREATE TABLE IF NOT EXISTS Topics (Id INTEGER PRIMARY KEY AUTOINCREMENT, SubjectId INTEGER, Name TEXT, Subtopics TEXT, IsCompleted INTEGER)",
            @"CREATE TABLE IF NOT EXISTS StudySessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, SubjectId INTEGER, Topic TEXT, Date TEXT, DurationMinutes INTEGER, SessionType INTEGER, Notes TEXT, ProductivityScore INTEGER)",
            @"CREATE TABLE IF NOT EXISTS PracticeRecords (Id INTEGER PRIMARY KEY AUTOINCREMENT, Platform TEXT, Title TEXT, Theme TEXT, Difficulty TEXT, Language TEXT, TimeSpentMinutes INTEGER, Status INTEGER, Notes TEXT, Link TEXT, Date TEXT)",
            @"CREATE TABLE IF NOT EXISTS Goals (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Period TEXT, TargetValue INTEGER, CurrentValue INTEGER)",
            @"CREATE TABLE IF NOT EXISTS AppSettings (Key TEXT PRIMARY KEY, Value TEXT)"
        };

        foreach (var sql in commands)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync();
        }
    }
}
