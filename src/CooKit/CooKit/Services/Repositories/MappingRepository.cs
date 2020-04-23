using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using CooKit.Models;

namespace CooKit.Services.Repositories
{
    public class MappingRepository<T, TDto> : IRepository<T> 
        where T : IEntity 
        where TDto : IEntity
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TDto> _dtoRepository;

        public MappingRepository(IMapper mapper, IRepository<TDto> repository)
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper;
            _dtoRepository = repository;
        }

        public async Task<IList<T>> GetAllEntries()
        {
            var dtos = await _dtoRepository.GetAllEntries();
            return dtos.Select(MapDtoToEntity).ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            var dto = await _dtoRepository.GetById(id);
            return MapDtoToEntity(dto);
        }

        public async Task<IList<T>> GetByIds(IEnumerable<Guid> ids)
        {
            var dtos = await _dtoRepository.GetByIds(ids);
            return dtos.Select(MapDtoToEntity).ToList();
        }

        public Task Add(T entity)
        {
            var dto = MapEntityToDto(entity);
            return _dtoRepository.Add(dto);
        }

        public Task Remove(T entity)
        {
            var dto = MapEntityToDto(entity);
            return _dtoRepository.Remove(dto);
        }

        public Task Update(T entity)
        {
            var dto = MapEntityToDto(entity);
            return _dtoRepository.Update(dto);
        }

        #region Helper Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T MapDtoToEntity(TDto dto)
        {
            return dto is null ? default : _mapper.Map<T>(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TDto MapEntityToDto(T entity)
        {
            return entity is null ? default : _mapper.Map<TDto>(entity);
        }

        #endregion
    }
}
