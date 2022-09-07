using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using MaFileRenamer.ViewModels;

namespace MaFileRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragDropContainer_OnDrop(object sender, DragEventArgs e)
        {
            try
            {
                var isDataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
                if (!isDataPresent)
                    throw new Exception("Unsupported data format");

                var data = e.Data.GetData(DataFormats.FileDrop);
                if (sender is FrameworkElement { DataContext: MainViewModel context })
                    context.ParseFilesCommand.Execute(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}