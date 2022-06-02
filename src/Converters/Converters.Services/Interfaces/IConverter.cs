namespace Converters.Services.Interfaces;

public interface IConverter
{
    byte[] Convert(Stream stream);
    string Convert(string filePath, out byte[] data);
}