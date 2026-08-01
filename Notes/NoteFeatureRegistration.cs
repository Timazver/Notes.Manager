using Notes.Manager.Notes.service;

namespace Notes.Manager.Notes;

public static class NoteFeatureRegistration
{
    public static IServiceCollection AddNoteFeature(this IServiceCollection services)
    {
        services.AddScoped<NoteService>();
        return services;
    }
}
