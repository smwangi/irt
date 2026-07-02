// Purpose: Contains tests for the Datasource controller.

using System.Text.Json;
using Irt.IntegrationTest.Setup;

namespace Irt.IntegrationTest.Tests;
public class DatasourceTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public DatasourceTests(CustomWebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Test1()
    {
        _factory.Intialize();
        var response = await _factory.HttpClient.GetAsync(Constants.DatasourceRoute);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(responseString);
        var root = document.RootElement;
        var value = root.GetProperty("value");
        Assert.True(value.ValueKind == JsonValueKind.Array);
        Assert.Empty(value.EnumerateArray());
    }
}