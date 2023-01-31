#if UNITY_EDITOR && !UNITY_SWITCH || UNITY_STANDALONE
using System.IO;
using System.Text;
using UnityEngine;

public class StandaloneSaveSystem : ISaveSystem
{
    const string savesExtension = "satoko";
    const string settingsExtension = "rika";

    public string LoadFile(string filename, Directories directory)
    {
        
        if(!TryGetDirectory(directory))
        {
            return string.Empty;
        }
        else
        {
            var path = GetFullPath(filename, directory);
            if (!File.Exists(path)) return string.Empty;
            string json = File.ReadAllText(path, Encoding.UTF8);
            return json;
        }
    }

    public void SaveFile(string filename, Directories directory, string json)
    {
        if (!TryGetDirectory(directory)) CreateDirectory(directory);
        var path = GetFullPath(filename, directory);
        if (!File.Exists(path)) File.Delete(path);
        File.WriteAllText(path, json, Encoding.UTF8);
    }

    public bool TryGetDirectory(Directories directory)
    {
        return Directory.Exists($"{Application.dataPath}/{directory.ToString()}");
    }

    void CreateDirectory(Directories directory)
    {
        Directory.CreateDirectory($"{Application.dataPath}/{directory.ToString()}");
    }

    string GetFullPath(string filename, Directories directory)
    {
        var fullpath = $"{Application.dataPath}/{directory.ToString()}/{filename}.";
        switch (directory)
        {
            case Directories.Saves:
                fullpath += savesExtension;
                break;
            case Directories.Settings:
                fullpath += settingsExtension;
                break;
        }
        Debug.Log(fullpath);
        return fullpath;
    }
}
#endif