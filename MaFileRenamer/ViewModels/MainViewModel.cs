using System;
using System.Reflection;
using System.Windows.Input;
using MaFileRenamer.Commands;

namespace MaFileRenamer.ViewModels;

public class MainViewModel : ViewModelBase
{
    public Version? Version => Assembly.GetExecutingAssembly().GetName().Version;

    public ICommand ParseFilesCommand { get; }

    public MainViewModel(ConvertMaFilesCommand convertMaFilesCommand)
    {
        ParseFilesCommand = new ParseFilesCommand(convertMaFilesCommand);
    }
}