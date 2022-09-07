using System.Threading.Tasks;

namespace MaFileRenamer.Commands;

public interface IConvertMaFilesCommand
{
    Task Execute(string[] files);
}