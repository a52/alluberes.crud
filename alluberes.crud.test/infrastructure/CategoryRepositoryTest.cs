namespace alluberes.crud.test.infrastructure;

using Xunit;
using Moq;
using FluentAssertions;
using alluberes.crud.infrastructure.persistence;
using alluberes.crud.domain.entities;
using Dapper;
using System.Data.Common;
using alluberes.crud.domain.exceptions;
using Microsoft.Data.Sqlite;

public class CategoryRepositoryTests_Sqlite : IAsyncLifetime
{
    private const string InMemoryConnectionString = "DataSource=:memory:";  // In-memory SQLite
    private DbConnection _connection;
    private CategoryRepository_Sqlite _repository;

    public CategoryRepositoryTests_Sqlite()
    {
        _connection = new SqliteConnection(InMemoryConnectionString);
        _repository = new CategoryRepository_Sqlite(InMemoryConnectionString); // Use the same connection string
    }

    public async Task InitializeAsync()
    {
        await _connection.OpenAsync();
        // Create the table within the in-memory database
        await _connection.ExecuteAsync(@"
            CREATE TABLE Category (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                IsActive INTEGER NOT NULL
            );
        ");
    }

    public async Task DisposeAsync()
    {
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }

/*

    [Fact]
    public async Task AddAsync_ValidCategory_AddsAndReturnsEntityWithId()
    {
        // Arrange
        var entity = new Category { Name = "Test Entity", IsActive = true };

        // Act
        var createdEntity = await _repository.AddAsync(entity);

        // Assert
        createdEntity.Should().NotBeNull();
        createdEntity.Id.Should().BeGreaterThan(0);
        createdEntity.Name.Should().Be("Test Entity");
        createdEntity.IsActive.Should().BeTrue();

        // Retrieve and verify from the database
        var retrievedEntity = await _repository.GetByIdAsync(createdEntity.Id);
        retrievedEntity.Should().BeEquivalentTo(createdEntity);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsEntity()
    {
        // Arrange
        var entity = new Category { Name = "Test GetById", IsActive = true };
        var createdEntity = await _repository.AddAsync(entity); // Use AddAsync to get the ID

        // Act
        var retrievedEntity = await _repository.GetByIdAsync(createdEntity.Id);

        // Assert
        retrievedEntity.Should().NotBeNull();
        retrievedEntity.Should().BeEquivalentTo(createdEntity);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(9999); // Non-existent ID

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        // Arrange
        var entity1 = new Category { Name = "Entity 1", IsActive = true };
        var entity2 = new Category { Name = "Entity 2", IsActive = false };
        await _repository.AddAsync(entity1); // Add directly
        await _repository.AddAsync(entity2);

        // Act
        var entities = await _repository.GetAllAsync();

        // Assert
        entities.Should().NotBeNull();
        entities.Should().HaveCount(2);
        entities.Should().ContainEquivalentOf(entity1, options => options.Excluding(e => e.Id)); // Exclude generated IDs during comparison
        entities.Should().ContainEquivalentOf(entity2, options => options.Excluding(e => e.Id));
    }

    [Fact]
    public async Task UpdateAsync_ValidEntity_UpdatesSuccessfully()
    {
        // Arrange
        var originalEntity = new Category { Name = "Original Name", IsActive = true };
        var createdEntity = await _repository.AddAsync(originalEntity);

        createdEntity.Name = "Updated Name";
        createdEntity.IsActive = false;

        // Act
        await _repository.UpdateAsync(createdEntity);

        // Assert
        var updatedEntity = await _repository.GetByIdAsync(createdEntity.Id);
        updatedEntity.Should().NotBeNull();
        updatedEntity.Name.Should().Be("Updated Name");
        updatedEntity.IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_NonExistingEntity_ThrowsNotFoundException()
    {
        // Arrange
        var nonExistentEntity = new Category { Id = 9999, Name = "Non-existent", IsActive = true };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _repository.UpdateAsync(nonExistentEntity));
    }
    [Fact]
    public async Task DeleteAsync_ExistingEntity_DeletesSuccessfully()
    {
        // Arrange
        var entityToDelete = new Category { Name = "To Delete", IsActive = true };
        var createdEntity = await _repository.AddAsync(entityToDelete);

        // Act
        await _repository.DeleteAsync(createdEntity.Id);

        // Assert
        var deletedEntity = await _repository.GetByIdAsync(createdEntity.Id);
        deletedEntity.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_NonExistingEntity_ThrowsNotFoundException()
    {
       // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _repository.DeleteAsync(9999)); // Non-existent ID
    }

*/

}