using System;
using System.IO;
using XamarinPO.Droid.Interfaces;
using XamarinPO.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(FilesManager))]
namespace XamarinPO.Droid.Interfaces
{
    public class FilesManager : IFilesManager
    {
        public void SaveText(string filename, string text)
        {
            try
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filename);
                File.WriteAllText(filePath, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public string LoadText(string filename)
        {
            try
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filename);
                return File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }
    }
}
