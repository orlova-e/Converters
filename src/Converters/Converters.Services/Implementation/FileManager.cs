using Converters.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FileOptions = Converters.Services.Configuration.FileOptions;

namespace Converters.Services.Implementation;

public class FileManager : IFileManager
{
    private readonly ILogger<FileManager> _logger;
    private readonly FileOptions _fileOptions;
    
    public FileManager(
        IOptions<FileOptions> options,
        ILogger<FileManager> logger)
    {
        _logger = logger;
        _fileOptions = options.Value;
    }
    
    public async Task<string> SaveAsync(string fileName, Stream stream)
    {
        var directory = Path.GetDirectoryName(_fileOptions.FilePath);
        
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var filePath = Path.Combine(directory, fileName);
        var nameCounted = filePath;
        var count = 0;

        while (File.Exists(nameCounted))
        {
            count++;
            nameCounted = $"{Path.Combine(directory, Path.GetFileNameWithoutExtension(filePath))} ({count}){Path.GetExtension(filePath)}";
        }

        filePath = nameCounted;


        stream.Position = 0;
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await stream.CopyToAsync(fileStream);
        }

        return filePath;
    }

    public async Task<string> ConvertAsync(string fileName)
    {
        var converter = ConverterFactory.GetConverter(fileName);
        var filePath = Path.Combine(_fileOptions.FilePath, fileName);

        var convertedPath = converter.Convert(filePath, out var fileConverted);
        await File.WriteAllBytesAsync(convertedPath, fileConverted);

        return convertedPath;
    }

    public Stream Get(string filename)
    {
        var filePath = Path.Combine(_fileOptions.FilePath, filename);
        return File.OpenRead(filePath);
    }
}