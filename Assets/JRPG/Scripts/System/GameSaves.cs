using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSaves 
{
    static ISaveSystem saveSystem = GetCurrentSystem();

    static ISaveSystem GetCurrentSystem()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return new StandaloneSaveSystem();
#elif UNITY_SWITCH && UNITY_EDITOR
#endif
    }

    public static void SaveFile(this string json, string filename, Directories directory)
    {
        saveSystem.SaveFile(filename, directory, json);
    }

    public static string LoadFile(string filename, Directories directory)
    {
        return saveSystem.LoadFile(filename, directory);
    }
}
