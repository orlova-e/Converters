namespace Converters.Domain.Services.Specs.Domain;

public static class Convertations
{
    public static Spec<Entities.Convertation> BySessionId(string sessionId) => new(x => x.SessionId == sessionId);
}