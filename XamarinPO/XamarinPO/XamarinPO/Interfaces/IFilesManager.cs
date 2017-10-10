namespace XamarinPO.Interfaces
{
    public interface IFilesManager
    {
        void SaveText(string filename, string text);

        string LoadText(string filename);
    }
}
