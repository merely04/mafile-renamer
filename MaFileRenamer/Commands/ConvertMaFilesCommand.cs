using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MaFileRenamer.Models;
using MaFileRenamer.Services;
using Newtonsoft.Json;

namespace MaFileRenamer.Commands;

public class ConvertMaFilesCommand : IConvertMaFilesCommand
{
    private readonly FileService _fileService;
    private readonly PaperService _paperService;

    public ConvertMaFilesCommand(FileService fileService, PaperService paperService)
    {
        _fileService = fileService;
        _paperService = paperService;
    }

    public async Task Execute(string[] files)
    {
        var outputPath = _fileService.OutputPath;
        var maFileExtension = _fileService.MaFileExtension;
        int errorCount = 0, successCount = 0;

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            foreach (var file in files)
            {
                if (!Path.GetExtension(file).Equals(maFileExtension, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                var fileInfo = new FileInfo(file);
                if (!fileInfo.Exists)
                    continue;

                var fileDirectory = fileInfo.DirectoryName;
                if (string.IsNullOrEmpty(fileDirectory))
                    continue;

                try
                {
                    var fileContent = await _fileService.ReadFileContentAsync(file);
                    var maFile = JsonConvert.DeserializeObject<MaFile>(fileContent);
                    if (maFile is null || string.IsNullOrEmpty(maFile.AccountName))
                        return;

                    var outputFileName = Path.Combine(outputPath, $"{maFile.AccountName}{maFileExtension}");
                    _fileService.CreateDirectory(outputPath);
                    File.Copy(file, outputFileName, true);

                    ++successCount;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    ++errorCount;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            stopwatch.Stop();

            _paperService.ShowPaper(outputPath, files.Length, errorCount, successCount, stopwatch.ElapsedMilliseconds);
        }
    }
}