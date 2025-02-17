namespace alluberes.crud.application.usecases;

using alluberes.crud.domain.entities;
using alluberes.crud.domain.interfaces;
using alluberes.crud.application.dtos;
using alluberes.crud.domain.exceptions;

public class GetCategoryUseCase
{
    private readonly ICategoryRepository _repository;
    public GetCategoryUseCase(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryDto> ExecuteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if(entity == null)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
        };
    }
}


//Implement other use cases (Update, Delete, GetAll) similarly.