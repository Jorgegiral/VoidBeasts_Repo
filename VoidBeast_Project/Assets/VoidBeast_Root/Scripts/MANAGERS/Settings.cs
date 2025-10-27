using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    [SerializeField] AudioSource soundFXObject;
    [SerializeField] AudioMixer audioMixer;
    
    void Awake()
    {
        if (instance == null) { instance = this; }
            
        DontDestroyOnLoad(gameObject);


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
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20f);

    }

}
