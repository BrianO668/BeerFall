using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] catchSound;
    public AudioClip[] missSound;
    public AudioClip bgm;
    public float slowDownVal;
    public float beerCatchVol;
    public float brianVol;
    public float bgmVol;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    void Awake()
    {
        Instance = this;

        AudioSource[] aSources = GetComponents<AudioSource>();

        musicSource = aSources[0];
        sfxSource = aSources[1];

        musicSource.clip = bgm;
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
        sfxSource.PlayOneShot(missSound[Random.Range(0, missSound.Length)], brianVol);
    }

    public void PlayGameOver()
    {
        
    }

    public void SlowBGM()
    {
        musicSource.pitch = slowDownVal;
    }
}
