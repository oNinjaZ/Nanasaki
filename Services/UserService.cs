using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Nanasaki.Data;
using Nanasaki.Models;

namespace Nanasaki.Services;

#nullable enable
public class UserService : IUserService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(User user)
    {
        var existingUser = await GetUserByIdAsync(user.Id);
        if (existingUser is not null)
        {
            return false;
        }

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Users(Id, Username, RegistrationDate)
            VALUES(@Id, @Username, @RegistrationDate)", user);
        return result > 0;

    }

    public async Task<bool> DeleteAsync(string id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"
            DELETE FROM Users
            WHERE Id = @Id", new { Id = id });
        return result > 0;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<User>("SELECT * From Users");
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(
            @"SELECT * FROM Users
            WHERE Id = @Id", new { Id = id }
            );
    }

    public async Task<bool> UpdateUsernameAsync(string id, string newUsername)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"
            UPDATE Users
            SET Username = @Username
            WHERE Id = @Id", new { Username = newUsername, Id = id });
        return result > 0;
    }
}
