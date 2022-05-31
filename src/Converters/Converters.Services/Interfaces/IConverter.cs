namespace Converters.Services.Interfaces;

public interface IConverter
{
    byte[] Convert(Stream stream);
    string Convert(FileStream fileStream, out byte[] data);
}