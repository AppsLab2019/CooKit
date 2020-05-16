using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Repositories
{
    public abstract class MappingRepository<T, TDto> : IRepository<T> 
        where T : IEntity 
        where TDto : IEntity
    {
        protected readonly IRepository<TDto> DtoRepository;

        protected MappingRepository(IRepository<TDto> repository)
        {
            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            DtoRepository = repository;
        }

        protected abstract Task<T> MapDtoToEntity(TDto dto);
        protected abstract Task<TDto> MapEntityToDto(T entity);

        public async Task<IList<T>> GetAllEntries()
        {
            var dtos = await DtoRepository.GetAllEntries();
            return await MapDtosToEntity(dtos);
        }

        public async Task<T> GetById(Guid id)
        {
            var dto = await DtoRepository.GetById(id);
            return await MapDtoToEntity(dto);
        }

        public async Task<IList<T>> GetByIds(IEnumerable<Guid> ids)
        {
            var dtos = await DtoRepository.GetByIds(ids);
            return await MapDtosToEntity(dtos);
        }

        public async Task Add(T entity)
        {
            var dto = await MapEntityToDto(entity);
            await DtoRepository.Add(dto);
        }

        public async Task Remove(T entity)
        {
            var dto = await MapEntityToDto(entity);
            await DtoRepository.Remove(dto);
        }

        public async Task Update(T entity)
        {
            var dto = await MapEntityToDto(entity);
            await DtoRepository.Update(dto);
        }

        private async Task<IList<T>> MapDtosToEntity(IEnumerable<TDto> dtos)
        {
            if (dtos is null)
                return null;

            var tasks = dtos.Select(MapDtoToEntity).ToArray();
            await Task.WhenAll(tasks);
            return tasks.Select(task => task.Result).ToArray();
        }
    }
}
