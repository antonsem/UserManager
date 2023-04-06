using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace UserManager.Helpers
{
    internal static class FileHelper
    {
        public static void SaveTextFile(string file, Window owner, string filter = "")
        {
            var openFile = new SaveFileDialog
            {
                Filter = filter
            };
            openFile.ShowDialog(owner);

            if (string.IsNullOrEmpty(openFile.FileName))
            {
                return;
            }

            File.WriteAllText(openFile.FileName, file);
        }
    }
}
