namespace Converters.Domain.Services.Specs.Domain;

public static class Convertations
{
    public static Spec<Entities.Convertation> ByName(string name) => new(x => x.Name == name);
}