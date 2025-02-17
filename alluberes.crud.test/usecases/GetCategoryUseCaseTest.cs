namespace alluberes.crud.test.usecases;


using Xunit;
using Moq;
using FluentAssertions;
using alluberes.crud.application.usecases;
using alluberes.crud.domain.interfaces;
using alluberes.crud.domain.entities;
using alluberes.crud.application.dtos;
using alluberes.crud.domain.exceptions;


public class GetCategoryUseCaseTest
{
    [Fact]
    public async Task ExecuteAsync_ValidInput_ReturnsCategory()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var useCase = new GetCategoryUseCase(mockRepository.Object);
        var expectedEntity = new Category { Id = 1, Name = "Test Category", IsActive = true };
        var returnedEntity = new Category { Id = 1, Name = "Test Category", IsActive = true };

        mockRepository.Setup(repo => repo.GetByIdAsync(1))
                      .ReturnsAsync(returnedEntity);

        // Act
        var result = await useCase.ExecuteAsync(1);



        // Assert
        result.Should().BeEquivalentTo(expectedEntity);
    }

    [Fact]
    public async Task ExecuteAsync_IdNotFound_ThrowNotFoundException()
    {
        var mockRepository = new Mock<ICategoryRepository>();
        var useCase = new GetCategoryUseCase(mockRepository.Object);
        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Category)null);

        await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecuteAsync(1));
    }


}