using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] catchSound;
    public AudioClip[] missSound;
    public AudioClip[] bgm;
    public float slowDownVal;
    public float beerCatchVol;
    public float brianVol;
    public float bgmVol;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetBGMPitch();
    }

    public bool MainMenuSceneCheck()
    {
        return (SceneManager.GetActiveScene().name == "MainMenu");
    }

    public void PlayBGM(int song)
    {
        musicSource.clip = bgm[song];
        musicSource.loop = true;
        musicSource.volume = bgmVol;
        musicSource.Play();
    }

    public void PlayCatch()
    {
        sfxSource.pitch = Random.Range(0.9f, 1.5f);
        sfxSource.PlayOneShot(catchSound[Random.Range(0, catchSound.Length)], beerCatchVol);
        sfxSource.pitch = 1f;

    }

    public void PlayMiss()
    {
        if (MainMenuSceneCheck()) { return; }
        sfxSource.PlayOneShot(missSound[Random.Range(0, missSound.Length)], brianVol);
    }

    public void ResetBGMPitch()
    {
        musicSource.pitch = 1f;
    }

    public void SpeedBGM(float pitch)
    {
        musicSource.pitch = pitch;
    }
    public void SlowBGM()
    {
        musicSource.pitch = slowDownVal;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
