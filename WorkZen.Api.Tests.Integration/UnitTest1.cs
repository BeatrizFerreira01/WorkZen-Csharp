using System.Net.Http.Json;
using WorkZen.Api.DTOs.Meditations;

namespace WorkZen.Api.Tests.Integration;

public class MeditationTests : IClassFixture<WorkZenApiFactory>
{
    private readonly HttpClient _client;

    public MeditationTests(WorkZenApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Meditacao()
    {
        var dto = new CreateMeditationDto
        {
            Title = "Meditação de Teste",
            Description = "Criada pelo teste de integração",
            Category = "Mindfulness",
            DurationMinutes = 10,
            IsPremium = false
        };

        // ACT
        var response = await _client.PostAsJsonAsync("/api/v1/meditations", dto);

        // ASSERT
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<MeditationDetailDto>();

        Assert.NotNull(created);
        Assert.Equal(dto.Title, created.Title);
        Assert.Equal(dto.Category, created.Category);
        Assert.Equal(dto.DurationMinutes, created.DurationMinutes);
        Assert.Equal(dto.IsPremium, created.IsPremium);
    }
}