namespace alluberes.crud.application.usecases;

using alluberes.crud.domain.entities;
using alluberes.crud.domain.interfaces;
using alluberes.crud.application.dtos;
using alluberes.crud.domain.exceptions;
using System.IO.Pipelines;

public class GetCategoryUseCase
{
    private readonly ICategoryRepository _repository;
    public GetCategoryUseCase(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public  Task<CategoryDto> ExecuteAsync(int id)
    {
        var result = new CategoryDto();

        /*
        var entity = await _repository.GetByIdAsync(id);

        if(entity == null)
        {
            throw new NotFoundException($"Category with id {id} not found");
        }


        result =  new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
        };

        */


        return result;

    }
}


//Implement other use cases (Update, Delete, GetAll) similarly.