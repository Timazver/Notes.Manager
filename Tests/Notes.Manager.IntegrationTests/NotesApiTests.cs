using System.Net;
using System.Net.Http.Json;

namespace Notes.Manager.IntegrationTests;

[Collection(IntegrationTestCollection.Name)]
public sealed class NotesApiTests(IntegrationTestFixture fixture)
{
    [Fact]
    public async Task Register_then_login_and_get_notes_returns_empty_list()
    {
        var email = NewEmail();
        var registerResponse = await fixture.Client.PostAsJsonAsync("/api/auth/register", new
        {
            firstName = "Test",
            lastName = "User",
            email,
            password = "User-password-123"
        });

        Assert.Equal(HttpStatusCode.Created, registerResponse.StatusCode);

        var token = await fixture.LoginAsync(email, "User-password-123");
        IntegrationTestFixture.Authorize(fixture.Client, token);

        var response = await fixture.Client.GetAsync("/api/notes");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<ApiResponse<List<NoteResponse>>>();
        Assert.NotNull(body?.Data);
        Assert.Empty(body.Data);
    }

    [Fact]
    public async Task Regular_user_cannot_get_admin_notes()
    {
        var email = NewEmail();
        await fixture.Client.PostAsJsonAsync("/api/auth/register", new
        {
            firstName = "Test",
            lastName = "User",
            email,
            password = "User-password-123"
        });

        var token = await fixture.LoginAsync(email, "User-password-123");
        IntegrationTestFixture.Authorize(fixture.Client, token);

        var response = await fixture.Client.GetAsync("/api/admin/notes");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Admin_can_get_notes_created_by_another_user()
    {
        var email = NewEmail();
        await fixture.Client.PostAsJsonAsync("/api/auth/register", new
        {
            firstName = "Test",
            lastName = "User",
            email,
            password = "User-password-123"
        });

        var userToken = await fixture.LoginAsync(email, "User-password-123");
        IntegrationTestFixture.Authorize(fixture.Client, userToken);
        var createResponse = await fixture.Client.PostAsJsonAsync("/api/notes", new
        {
            title = "Integration note",
            content = "Created through HTTP"
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var adminToken = await fixture.LoginAsAdminAsync();
        IntegrationTestFixture.Authorize(fixture.Client, adminToken);

        var response = await fixture.Client.GetAsync("/api/admin/notes");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<ApiResponse<List<NoteResponse>>>();
        Assert.Contains(body!.Data!, note => note.Title == "Integration note");
    }

    private static string NewEmail()
    {
        return $"u-{Guid.NewGuid().ToString("N")[..8]}@example.com";
    }
}
