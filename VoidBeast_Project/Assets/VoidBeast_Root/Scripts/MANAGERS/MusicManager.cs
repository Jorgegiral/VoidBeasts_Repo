using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource musicSource;
    [SerializeField] AudioClip nightSong;
    [SerializeField] AudioClip daySong;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }
    public void PlayNightSong()
    {
        musicSource.clip = nightSong;
        musicSource.Play();
    }

    public void PlayDaySong()
    {
        musicSource.clip = daySong;
        musicSource.Play();
    }
    public void StopSong()
    {
        musicSource.Stop();
    }
}
