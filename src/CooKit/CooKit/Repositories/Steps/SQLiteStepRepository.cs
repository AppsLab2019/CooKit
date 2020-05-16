using System;
using System.Threading.Tasks;
using CooKit.Models.Steps;

namespace CooKit.Repositories.Steps
{
    public sealed class SQLiteStepRepository : MappingRepository<IStep, SQLiteRawStepDto>, IStepRepository
    {
        public SQLiteStepRepository(ISQLiteRawStepDtoRepository repository) : base(repository)
        {
        }

        #region Dto => Entity

        protected override Task<IStep> MapDtoToEntity(SQLiteRawStepDto dto)
        {
            IStep step = dto.Type switch
            {
                StepType.Text => InternalDtoToTextStep(dto),
                StepType.Image => InternalDtoToImageStep(dto),

                _ => throw new ArgumentOutOfRangeException(nameof(dto))
            };

            return Task.FromResult(step);
        }

        private static ITextStep InternalDtoToTextStep(SQLiteRawStepDto dto)
        {
            return new TextStep
            {
                Id = dto.Id,
                Text = dto.Data
            };
        }

        private static IImageStep InternalDtoToImageStep(SQLiteRawStepDto dto)
        {
            return new ImageStep
            {
                Id = dto.Id,
                Image = dto.Data
            };
        }

        #endregion

        #region Entity => Dto

        protected override Task<SQLiteRawStepDto> MapEntityToDto(IStep entity)
        {
            var dto = entity switch
            {
                ITextStep textStep => InternalStepToDto(textStep),
                IImageStep imageStep => InternalStepToDto(imageStep),

                _ => throw new ArgumentOutOfRangeException(nameof(entity))
            };

            return Task.FromResult(dto);
        }

        private static SQLiteRawStepDto InternalStepToDto(ITextStep step)
        {
            return new SQLiteRawStepDto
            {
                Id = step.Id,
                Type = StepType.Text,
                Data = step.Text
            };
        }

        private static SQLiteRawStepDto InternalStepToDto(IImageStep imageStep)
        {
            return new SQLiteRawStepDto
            {
                Id = imageStep.Id,
                Type = StepType.Image,
                Data = imageStep.Image
            };
        }

        #endregion
    }
}
