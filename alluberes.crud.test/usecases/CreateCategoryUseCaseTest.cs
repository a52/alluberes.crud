namespace alluberes.crud.test.usecases;

using Xunit;
using Moq;
using FluentAssertions;
using alluberes.crud.application.usecases;
using alluberes.crud.domain.interfaces;
using alluberes.crud.domain.entities;
using alluberes.crud.application.dtos;


public class CreateCategoryUseCaseTest 
{
    [Fact]
    public async Task ExecuteAsync_ValidInput_CreatesCategory()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var useCase = new CreateCategoryUseCase(mockRepository.Object);
        var input = new CreateCategoryDto { Name = "Test Category", IsActive = true };
        var createdCategory = new Category { Id = 1, Name = "Test Category", IsActive = true };

        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
                      .ReturnsAsync(createdCategory);

        // Act
        var result = await useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Test Category");
        result.IsActive.Should().BeTrue();
        mockRepository.Verify(repo => repo.AddAsync(It.Is<Category>(c => c.Name == "Test Category" && c.IsActive)), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var useCase = new CreateCategoryUseCase(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => useCase.ExecuteAsync(null));
    }

    [Fact]
    public async Task ExecuteAsync_EmptyName_ThrowsArgumentException()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var useCase = new CreateCategoryUseCase(mockRepository.Object);
        var input = new CreateCategoryDto { Name = "", IsActive = true };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(input));
    }
}
