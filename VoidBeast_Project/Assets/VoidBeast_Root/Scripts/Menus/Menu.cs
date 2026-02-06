using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [Header("Background References")]
    [SerializeField] Image backgroundImage;
    [SerializeField] Sprite backgroundDay;
    [SerializeField] Sprite backgroundNight;
    [SerializeField] GameObject logoImages;

    [Header("Panels References")]
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject generalSettings;
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject ControllerControls;
    [SerializeField] GameObject leaderboardPanel;

    [Header("Slider References")]
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider musicSlider;

    [Header("SFX References")]
    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioClip unClickSound;
    private float sfxVolume;
    private float musicVolume;

    [Header("Controller References")]
    [SerializeField] GameObject firstSelectedMenu;
    [SerializeField] GameObject firstSelectedControls;
    [SerializeField] GameObject firstSelectedGame;


    private bool idiomaActivado = false;
    private int idiomaActual = 0;


    private void Start()
    {
        logoImages.SetActive(true);
        settingsPanel.SetActive(false);
        int numRandom = UnityEngine.Random.Range(0, 2);
        if (numRandom == 0)
        {
            backgroundImage.sprite = backgroundNight;
            MusicManager.instance.PlayNightSong();
        }
        if (numRandom == 1)
        { 
            backgroundImage.sprite = backgroundDay;
            MusicManager.instance.PlayDaySong();

        }
        int id = PlayerPrefs.GetInt("LocaleKey", 0);
        idiomaActual = id;
        StartCoroutine(SetIdLocal(id));
        musicSlider.value = Settings.instance.GetMusicVolume();
        SFXSlider.value = Settings.instance.GetSFXVolume();
        StartCoroutine(SelectFirstButtonDelayed());

    }

    public void ExitButton()
    {
       Application.Quit();
    }
    public void PlayButton()
    {
        Time.timeScale = 1f;
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);
        SceneManager.LoadScene(1);

    }
    public void TutorialButton()
    {
        Time.timeScale = 1f;
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);
        SceneManager.LoadScene(2);

    }
    public void OptionsButton()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        settingsPanel.SetActive(true);
        logoImages.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelectedMenu);
    }
    public void CloseOptions()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

        settingsPanel.SetActive(false);
        logoImages.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelectedGame);

    }
    public void ShowControls()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        controlsPanel.SetActive(true);
        keyboardControls.SetActive(true);
        ControllerControls.SetActive(false);
        generalSettings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelectedControls);

    }
    public void CloseControls()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

        controlsPanel.SetActive(false);
        generalSettings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelectedMenu);

    }
    public void ShowKeyboardControls()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        keyboardControls.SetActive(true);
        ControllerControls.SetActive(false);
    }
    public void ShowControllerControls()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        keyboardControls.SetActive(false);
        ControllerControls.SetActive(true);
    }
    public void SetSFX()
    {
        sfxVolume = SFXSlider.value;
        Settings.instance.SetSFXVolume(sfxVolume);
    }
    public void SetMusic()
    {
        musicVolume = musicSlider.value;
        Settings.instance.SetMusicVolume(musicVolume);
    }
    public void CambiarIdiomas()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        if (idiomaActivado)
            return;

        idiomaActual++;

        if (idiomaActual >= LocalizationSettings.AvailableLocales.Locales.Count)
            idiomaActual = 0;

        StartCoroutine(SetIdLocal(idiomaActual));
    }
    private IEnumerator SetIdLocal(int localId)
    {
        idiomaActivado = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localId];
        PlayerPrefs.SetInt("LocaleKey",localId);
        PlayerPrefs.Save();
        idiomaActivado = false;
    }
    private IEnumerator SelectFirstButtonDelayed()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedGame);
    }
    public void OpenLeaderboard()
    {
        leaderboardPanel.SetActive(true);
    }
    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }
    //QUITAR LUEGO

    public void EscenaDeTest()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);

    }
}
