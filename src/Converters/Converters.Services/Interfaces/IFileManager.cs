namespace Converters.Services.Interfaces;

public interface IFileManager
{
    Task<string> SaveAsync(string fileName, Stream stream);
    Task<string> ConvertAsync(string fileName);
}