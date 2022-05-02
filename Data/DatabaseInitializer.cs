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
                Username TEXT NOY NULL,
                RegistrationDate TEXT NOT NULL)"
            );
    }
}