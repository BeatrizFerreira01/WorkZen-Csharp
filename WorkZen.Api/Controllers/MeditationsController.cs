using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WorkZen.Api.Data;
using WorkZen.Api.DTOs;
using WorkZen.Api.DTOs.Meditations;
using WorkZen.Api.Entities;
using WorkZen.Api.Services.Interfaces;

namespace WorkZen.Api.Services;

public class MeditationService : IMeditationService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _http;
    private readonly ILogger<MeditationService> _logger;

    public MeditationService(AppDbContext context,
                             IHttpContextAccessor http,
                             ILogger<MeditationService> logger)
    {
        _context = context;
        _http = http;
        _logger = logger;
    }

    // ============================================================
    // GET ALL (Paginated + HATEOAS)
    // ============================================================

    public async Task<PaginatedResponseDto<MeditationListItemDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _context.Meditations.AsNoTracking();

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .OrderBy(m => m.Title)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MeditationListItemDto
            {
                Id = m.Id,
                Title = m.Title,
                Category = m.Category,
                DurationMinutes = m.DurationMinutes,
                IsPremium = m.IsPremium
            })
            .ToListAsync();

        var links = BuildLinks("meditations", pageNumber, totalPages);

        return new PaginatedResponseDto<MeditationListItemDto>(
            items,
            pageNumber,
            pageSize,
            totalItems,
            totalPages,
            links
        );
    }

    // ============================================================
    // GET BY ID
    // ============================================================

    public async Task<MeditationDetailDto?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Meditations
            .AsNoTracking()
            .Include(m => m.Sessions)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (entity == null)
            return null;

        return new MeditationDetailDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Category = entity.Category,
            DurationMinutes = entity.DurationMinutes,
            IsPremium = entity.IsPremium,
            Sessions = entity.Sessions.Select(s => new SessionDto
            {
                Id = s.Id,
                StartedAt = s.StartedAt,
                Rating = s.Rating,
                Mood = s.Mood
            }).ToList()
        };
    }

    // ============================================================
    // CREATE
    // ============================================================

    public async Task<MeditationDetailDto> CreateAsync(CreateMeditationDto dto)
    {
        var entity = new Meditation
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            DurationMinutes = dto.DurationMinutes,
            IsPremium = dto.IsPremium
        };

        _context.Meditations.Add(entity);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Meditation created: {Id}", entity.Id);

        return new MeditationDetailDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Category = entity.Category,
            DurationMinutes = entity.DurationMinutes,
            IsPremium = entity.IsPremium,
            Sessions = new List<SessionDto>()
        };
    }

    // ============================================================
    // UPDATE
    // ============================================================

    public async Task<bool> UpdateAsync(Guid id, UpdateMeditationDto dto)
    {
        var entity = await _context.Meditations.FirstOrDefaultAsync(m => m.Id == id);

        if (entity == null)
            return false;

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.DurationMinutes = dto.DurationMinutes;
        entity.Category = dto.Category;
        entity.IsPremium = dto.IsPremium;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Meditation updated: {Id}", id);

        return true;
    }

    // ============================================================
    // DELETE
    // ============================================================

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Meditations.FirstOrDefaultAsync(m => m.Id == id);

        if (entity == null)
            return false;

        _context.Meditations.Remove(entity);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Meditation deleted: {Id}", id);
        return true;
    }

    // ============================================================
    // HATEOAS LINKS
    // ============================================================

    private IEnumerable<LinkDto> BuildLinks(string route, int page, int totalPages)
    {
        var http = _http.HttpContext;

        if (http == null)
            return new List<LinkDto>();

        var request = http.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

        var links = new List<LinkDto>
        {
            new($"{baseUrl}/{route}?page={page}", "self", "GET")
        };

        if (page > 1)
            links.Add(new($"{baseUrl}/{route}?page={page - 1}", "prev", "GET"));

        if (page < totalPages)
            links.Add(new($"{baseUrl}/{route}?page={page + 1}", "next", "GET"));

        return links;
    }
}