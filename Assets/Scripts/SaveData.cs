using UnityEngine;

[System.Serializable] //Tells Unity that the class can be converted to data
public class SaveData
{
    public int highScore = 0;
    public int selectedBGM = 0;
    public int unlockedSongsCount = 2;
    public int selectedSong = 0;
}
