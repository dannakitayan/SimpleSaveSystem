using Newtonsoft.Json;

[System.Serializable]
public struct Settings
{
    float backgroundVolume;
    float soundsVolume;
    float voicesVolume;
    //Languages currentLanguage;

    public float BackgroundVolume { set { backgroundVolume = value; } get { return backgroundVolume; } }
    public float SoundsVolume { set { soundsVolume = value; } get { return soundsVolume; } }
    public float VoicesVolume { set { voicesVolume = value; } get { return voicesVolume; } }

    public void SetUp(Settings instance)
    {
        backgroundVolume = instance.backgroundVolume;
        soundsVolume = instance.soundsVolume;
        voicesVolume = instance.voicesVolume;
        //currentLanguage = instance.currentLanguage;
    }
}

public static class GameSettings
{
    public static Settings Values = new Settings() { BackgroundVolume = 1f, SoundsVolume = 1f, VoicesVolume = 1f};

    public static void SaveSettings()
    {
        var json = JsonConvert.SerializeObject(Values);
        json.SaveFile("furude", Directories.Settings);
    }

    public static void LoadSettings()
    {
        var json = GameSaves.LoadFile("furude", Directories.Settings);
        if(json == string.Empty)
        {
            SaveSettings();
        }
        else
        {
            var instance = JsonConvert.DeserializeObject<Settings>(json);
            Values.SetUp(instance);
        }
    }
}
