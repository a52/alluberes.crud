// MyProject.Infrastructure/Persistence/MyEntityRepository_Sqlite.cs
namespace alluberes.crud.infrastructure.persistence;

using alluberes.crud.domain.entities;
using alluberes.crud.domain.interfaces;
using Microsoft.Data.Sqlite; // Use Microsoft.Data.Sqlite
using Dapper;
using alluberes.crud.domain.exceptions;
using System.Data.Common;

public class CategoryRepository_Sqlite : ICategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository_Sqlite(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(); //Explicitly open the connection.
        const string sql = "SELECT Id, Name, IsActive FROM Category WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = "SELECT Id, Name, IsActive FROM Category";
        return await connection.QueryAsync<Category>(sql);
    }

    public async Task<Category> AddAsync(Category entity)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = "INSERT INTO Category (Name, IsActive) VALUES (@Name, @IsActive); SELECT last_insert_rowid();";
        var id = await connection.ExecuteScalarAsync<int>(sql, new { entity.Name, entity.IsActive });
        entity.Id = id;
        return entity;
    }

    public async Task UpdateAsync(Category entity)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = "UPDATE Category SET Name = @Name, IsActive = @IsActive WHERE Id = @Id";
        var affectedRows = await connection.ExecuteAsync(sql, new { entity.Name, entity.IsActive, entity.Id });
        if (affectedRows == 0)
        {
            throw new NotFoundException($"Category with id {entity.Id} not found");
        }
    }

    public async Task DeleteAsync(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = "DELETE FROM Category WHERE Id = @Id";
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        if (affectedRows == 0)
        {
            throw new NotFoundException($"Category with Id {id} Not Found");
        }
    }
}