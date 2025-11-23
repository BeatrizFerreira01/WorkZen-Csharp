using WorkZen.Api.DTOs;
using WorkZen.Api.DTOs.Meditations;

namespace WorkZen.Api.Services.Interfaces
{
    public interface IMeditationService
    {
        Task<PaginatedResponseDto<MeditationListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<MeditationDetailDto?> GetByIdAsync(Guid id);
        Task<MeditationDetailDto> CreateAsync(CreateMeditationDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateMeditationDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}