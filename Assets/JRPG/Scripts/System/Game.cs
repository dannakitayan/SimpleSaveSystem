using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public static class Game
{
    static Dictionary<string, object> gameContent = new Dictionary<string, object>();

    public static Action onLoadAllContent, onSaveAllContent;

    public static void AddContent(string name, object value)
    {
        if (gameContent.ContainsKey(name)) gameContent.Remove(name);
        if (value is UnityEngine.Vector3) value = ((UnityEngine.Vector3)value).Vector3ToArray();
        gameContent.Add(name, value);
    }

    public static object GetContent(string name)
    {
        object value = null;
        gameContent.TryGetValue(name, out value);
        return value;
    }

    public static void SaveProgress(string filename)
    {
        onSaveAllContent?.Invoke();
        var json = JsonConvert.SerializeObject(gameContent, Formatting.Indented);
        json.SaveFile(filename, Directories.Saves);
    }

    public static void LoadProgress(string filename)
    {
        var json = GameSaves.LoadFile(filename, Directories.Saves);
        if (json == string.Empty)
        {
            return;
        }
        else
        {
            var instance = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            gameContent.Clear();
            gameContent = instance.JsonDictionaryToGameDictionary();
            //if a game is loaded => onLoadAllContent?.Invoke();
        }
    }

#if DEBUG_SAVE
    public static void ShowKeys()
    {
        foreach(var key in gameContent)
        {
            UnityEngine.Debug.Log($"{key.Key} -> {key.Value.ToString()}");
        }
    }
#endif
}

public static class ConverToSimple
{
    public static float[] Vector3ToArray(this UnityEngine.Vector3 vector3)
    {
        return new float[3] {vector3.x, vector3.y, vector3.z };
    }

    public static UnityEngine.Vector3 ArrayToVector3(this float[] array)
    {
        return new UnityEngine.Vector3(array[0], array[1], array[2]);
    }

    public static Dictionary<string, object> JsonDictionaryToGameDictionary(this Dictionary<string, object> dictionary)
    {
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        foreach(var element in dictionary)
        {
            if(element.Value is Newtonsoft.Json.Linq.JArray)
            {
                var newValue = ((Newtonsoft.Json.Linq.JArray)element.Value).ToObject<float[]>().ArrayToVector3();
                newDictionary.Add(element.Key, newValue);
                continue;
            }
            newDictionary.Add(element.Key, element.Value);
        }
        return newDictionary;
    }
}
