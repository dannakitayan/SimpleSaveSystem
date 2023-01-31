public enum Directories
{
    Saves,
    Settings
}

public interface ISaveSystem
{
    public void SaveFile(string filename, Directories directory, string json);
    public string LoadFile(string filename, Directories directory);
    public bool TryGetDirectory(Directories directory);

}
