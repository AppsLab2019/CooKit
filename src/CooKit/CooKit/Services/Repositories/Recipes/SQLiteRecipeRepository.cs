using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CooKit.Models.Recipes;

namespace CooKit.Services.Repositories.Recipes
{
    public sealed class SQLiteRecipeRepository : IRecipeRepository
    {
        private readonly ISQLiteRecipeDtoRepository _dtoRepository;
        private readonly IMapper _mapper;

        public SQLiteRecipeRepository(ISQLiteRecipeDtoRepository repository, IMapper mapper)
        {
            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            _dtoRepository = repository;
            _mapper = mapper;
        }

        public async Task<IList<IRecipe>> GetAllEntries()
        {
            var dtos = await _dtoRepository.GetAllEntries();
            return dtos.Select(MapDtoToRecipe).ToList();
        }

        public async Task<IRecipe> GetById(Guid id)
        {
            var dto = await _dtoRepository.GetById(id);
            return MapDtoToRecipe(dto);
        }

        public async Task<IList<IRecipe>> GetByIds(IEnumerable<Guid> ids)
        {
            var dtos = await _dtoRepository.GetByIds(ids);
            return dtos.Select(MapDtoToRecipe).ToList();
        }

        public Task Add(IRecipe entity)
        {
            var dto = MapRecipeToDto(entity);
            return _dtoRepository.Add(dto);
        }

        public Task Remove(IRecipe entity)
        {
            var dto = MapRecipeToDto(entity);
            return _dtoRepository.Remove(dto);
        }

        public Task Update(IRecipe entity)
        {
            var dto = MapRecipeToDto(entity);
            return _dtoRepository.Update(dto);
        }

        private IRecipe MapDtoToRecipe(SQLiteRecipeDto dto) =>
            _mapper.Map<IRecipe>(dto);

        private SQLiteRecipeDto MapRecipeToDto(IRecipe recipe) =>
            _mapper.Map<SQLiteRecipeDto>(recipe);
    }
}
