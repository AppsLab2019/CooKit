using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using CooKit.Models;

namespace CooKit.Repositories
{
    public class MappingRepository<T, TDto> : IRepository<T> 
        where T : IEntity 
        where TDto : IEntity
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<TDto> DtoRepository;

        public MappingRepository(IMapper mapper, IRepository<TDto> repository)
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            Mapper = mapper;
            DtoRepository = repository;
        }

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

        #region Helper Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Task<T> MapDtoToEntity(TDto dto)
        {
            var entity = dto is null ? default : Mapper.Map<T>(dto);
            return Task.FromResult(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Task<TDto> MapEntityToDto(T entity)
        {
            var dto = entity is null ? default : Mapper.Map<TDto>(entity);
            return Task.FromResult(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async Task<IList<T>> MapDtosToEntity(IEnumerable<TDto> dtos)
        {
            if (dtos is null)
                return null;

            var tasks = dtos.Select(MapDtoToEntity).ToArray();
            await Task.WhenAll(tasks);
            return tasks.Select(task => task.Result).ToArray();
        }

        #endregion
    }
}
