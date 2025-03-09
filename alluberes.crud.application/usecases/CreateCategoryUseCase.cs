// MyProject.Application/UseCases/CreateMyEntityUseCase.cs
namespace alluberes.crud.application.usecases;

using alluberes.crud.domain.entities;
using alluberes.crud.domain.interfaces;
using alluberes.crud.application.dtos;

public class CreateCategoryUseCase
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryUseCase(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /*
    public async Task<CategoryDto> ExecuteAsync(CreateCategoryDto input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        if (string.IsNullOrWhiteSpace(input.Name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(input.Name));
        }

        var entity = new Category
        {
            Name = input.Name,
            IsActive = input.IsActive
        };

        var createdEntity = await _repository.AddAsync(entity);
         return new CategoryDto
        {
             Id = createdEntity.Id,
             Name = createdEntity.Name,
             IsActive = createdEntity.IsActive,
        };
    }

    */
}