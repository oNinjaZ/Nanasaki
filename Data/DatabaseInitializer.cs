using System.Threading.Tasks;
using Dapper;

namespace Nanasaki.Data;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS Users(
                Id TEXT PRIMARY KEY,
                Username TEXT NOT NULL,
                RegistrationDate TEXT NOT NULL);
             CREATE TABLE IF NOT EXISTS BookLogs(
                Id TEXT PRIMARY KEY,
                PagesRead INTEGER,
                LogDate TEXT NOT NULL);
                "
            );
    }
}