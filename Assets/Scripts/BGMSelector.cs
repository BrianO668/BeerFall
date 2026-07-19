using TMPro;
using UnityEngine;

public class BGMSelector : MonoBehaviour
{
    public TMP_Text songText;

    private int currentSong;

    private void Start()
    {
        currentSong = SaveManager.Instance.saveData.selectedSong;
        AudioManager.Instance.PlayBGM(currentSong);
        UpdateSongText();
    }

    public void UpdateSongText()
    {
        songText.text = AudioManager.Instance.bgm[currentSong].name;
    }

    public void NextSong()
    {
        currentSong++;

        if (currentSong >= SaveManager.Instance.saveData.unlockedSongsCount)
        {
            currentSong = 0;
        }

        SaveManager.Instance.saveData.selectedSong = currentSong;
        SaveManager.Instance.SaveGame();

        UpdateSongText();
        AudioManager.Instance.PlayBGM(currentSong);
    }

    public void PrevSong()
    {
        currentSong--;

        if (currentSong < 0)
        {
            currentSong = SaveManager.Instance.saveData.unlockedSongsCount - 1;
        }

        SaveManager.Instance.saveData.selectedSong = currentSong;
        SaveManager.Instance.SaveGame();

        UpdateSongText();
        AudioManager.Instance.PlayBGM(currentSong);
    }

    public int GetCurrentSong()
    {
        return currentSong;
    }
}
