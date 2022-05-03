using System.Threading.Tasks;
using Dapper;
using Nanasaki.Data;
using Nanasaki.Models;

namespace Nanasaki.Services;

public class BookLogService : IBookLogService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public BookLogService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(BookLog bookLog)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"
        INSERT INTO BookLogs(Id, User, PagesRead, LogDate)
        VALUES(@Id, @User, @PagesRead, @LogDate)", bookLog);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"
            DELETE FROM BookLogs
            WHERE Id = @Id
        ", new { Id = id });
        return result > 0;

    }
}