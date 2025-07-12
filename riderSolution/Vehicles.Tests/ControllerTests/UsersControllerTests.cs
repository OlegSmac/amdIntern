using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Vehicles.API;
using Vehicles.API.Contracts.DTOs.Vehicles;
using Xunit;

namespace Vehicles.Tests.ControllerTests;

public class VehiclesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public VehiclesControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllVehicles_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/vehicles");
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<List<VehicleDTO>>();
        content.Should().NotBeNull();
        content.Count.Should().BeGreaterThan(0);
        content.Should().AllBeOfType<VehicleDTO>();
        content.Should().Contain(v => v.Brand == "Mercedes");
        content.Should().Contain(v => v.Brand == "Yamaha");
    }
}
