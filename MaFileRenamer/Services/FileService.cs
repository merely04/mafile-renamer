using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MaFileRenamer.Services;

public class FileService
{
    private readonly string _currentDirectory;

    public FileService()
    {
        _currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                            Environment.CurrentDirectory;
    }

    public string MaFileExtension => ".maFile";

    public string OutputPath => Path.Combine(_currentDirectory, "output");

    public void CreateDirectory(string directory)
    {
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }

    public async Task<string> ReadFileContentAsync(string fileName)
    {
        await using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
        using var sr = new StreamReader(fs);
        return await sr.ReadToEndAsync();
    }
}