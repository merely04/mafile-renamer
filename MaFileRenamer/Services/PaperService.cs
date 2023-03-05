using System.Diagnostics;
using System.Text;
using System.Windows;

namespace MaFileRenamer.Services;

public class PaperService
{
    public void ShowPaper(string outputPath, int filesCount, int errorCount, int successCount, long elapsedTime)
    {
        var paper = new StringBuilder();
        paper.AppendLine($"Total: {filesCount}");
        paper.AppendLine($"Error: {errorCount}");
        paper.AppendLine($"Success: {successCount}");
        paper.AppendLine();
        paper.AppendLine($"Elapsed time: {elapsedTime} ms");
        
        if (successCount > 0)
            Process.Start("explorer", outputPath);
        
        MessageBox.Show(paper.ToString(), "Result", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}