using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public int maxSongs;
    public SaveData saveData;
    public string savePath;
    void Awake()
    {
        if (Instance != null) //If this Instance already exists, keep it alive and destroy me instead
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/save.json"; //Create save path

        LoadGame();
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath); //If file exists, read it and push it into saveData

            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData); //Turn the JSON script into a string
        File.WriteAllText(savePath, json); //Write the data to the save path
    }

    public bool UnlockSong()
    {
        if (saveData.unlockedSongsCount < maxSongs)
        {
            saveData.unlockedSongsCount++;
            return true;
        }

        return false;
    }
}
