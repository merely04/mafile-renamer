using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MaFileRenamer.Commands;

public class ParseFilesCommand : AsyncCommandBase
{
    private readonly ConvertMaFilesCommand _convertMaFilesCommand;

    public ParseFilesCommand(ConvertMaFilesCommand convertMaFilesCommand)
    {
        _convertMaFilesCommand = convertMaFilesCommand;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var ofd = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                RestoreDirectory = false,
                Multiselect = false,
                Title = "Select maFiles path"
            };

            if (parameter is not string[] files)
            {
                if (ofd.ShowDialog() is CommonFileDialogResult.None or CommonFileDialogResult.Cancel)
                    return;
                
                files = ofd.FileNames.ToArray();
                if (files is null || files.Length < 1)
                    throw new Exception("Files not found");
            }

            var fileNames = new List<string>();
            foreach (var file in files)
            {
                var attributes = File.GetAttributes(file);
                if (attributes.HasFlag(FileAttributes.Directory))
                    fileNames.AddRange(Directory.GetFiles(file));
                else
                    fileNames.Add(file);
            }

            await _convertMaFilesCommand.Execute(fileNames.ToArray());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
