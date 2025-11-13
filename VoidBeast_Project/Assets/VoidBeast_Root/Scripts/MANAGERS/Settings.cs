using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    [SerializeField] AudioSource soundFXObject;
    [SerializeField] AudioMixer audioMixer;
    private AudioSource uniqueAudioSource;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float musicVolume= 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform,float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position,Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlayRandomSoundFXClip(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, clip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        // En caso de que haya varios sonidos de la misma cosa que los haga random
    }
    public void PlayUniqueSoundSFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        if (uniqueAudioSource != null && uniqueAudioSource.isPlaying)
            return;

        if (uniqueAudioSource == null)
        {
            uniqueAudioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        }

        uniqueAudioSource.transform.position = spawnTransform.position;
        uniqueAudioSource.clip = clip;
        uniqueAudioSource.volume = volume;
        uniqueAudioSource.Play();
    }
    public void StopSingleSoundFX()
    {
        if (uniqueAudioSource != null && uniqueAudioSource.isPlaying)
        {
            uniqueAudioSource.Stop();
        }
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20f);

    }
    public float GetSFXVolume()
    {
        return sfxVolume;
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
