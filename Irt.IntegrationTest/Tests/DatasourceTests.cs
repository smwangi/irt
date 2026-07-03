// Purpose: Contains tests for the Datasource controller.

using System.Text.Json;
using Irt.IntegrationTest.Setup;

namespace Irt.IntegrationTest.Tests;
public class DatasourceTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    [Fact]
    public async Task Test1()
    {
        factory.Intialize();
        var response = await factory.HttpClient.GetAsync(Constants.DatasourceRoute);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(responseString);
        var root = document.RootElement;
        var value = root.ValueKind == JsonValueKind.Object
            ? root.GetProperty("value")
            : root;
        Assert.True(value.ValueKind == JsonValueKind.Array);
        Assert.Empty(value.EnumerateArray());
    }
}
