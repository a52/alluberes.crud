namespace alluberes.crud.infrastructure.persistence;

using alluberes.crud.domain.entities;
using alluberes.crud.domain.interfaces;
using MySqlConnector;
using Dapper;
using alluberes.crud.domain.exceptions;

public class CategoryRepository : ICategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        string query = "SELECT Id, Name, IsActive FROM Category WHERE Id = @Id";
        var category = await connection.QueryFirstOrDefaultAsync<Category>(query, new { Id = id });
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string sql = "SELECT Id, Name, IsActive FROM Category";
        return await connection.QueryAsync<Category>(sql);
    }

    public async Task<Category> AddAsync(Category entity)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string sql = "INSERT INTO Category (Name, IsActive) VALUES (@Name, @IsActive); SELECT LAST_INSERT_ID();";
        var id = await connection.ExecuteScalarAsync<int>(sql, entity);
        entity.Id = id; // Set the generated ID.  Crucial!
        return entity;
    }

    public async Task UpdateAsync(Category entity)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string sql = "UPDATE Category SET Name = @Name, IsActive = @IsActive WHERE Id = @Id";
        var affectedRows = await connection.ExecuteAsync(sql, entity);
        if(affectedRows == 0)
        {
            throw new NotFoundException($"Category with id {entity.Id} not found");
        }
    }

    public async Task DeleteAsync(int id)
    {
        await using var connection = new MySqlConnection(_connectionString);
        const string sql = "DELETE FROM Category WHERE Id = @Id";
        var affectedRows =  await connection.ExecuteAsync(sql, new { Id = id });
        if (affectedRows == 0)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }
    }



}