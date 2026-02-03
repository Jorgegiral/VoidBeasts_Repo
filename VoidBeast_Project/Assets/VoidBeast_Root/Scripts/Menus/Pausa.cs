using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.EventSystems;
public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject generalSettings;
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject ControllerControls;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioClip unClickSound;
    [SerializeField] GameObject firstSelectedOnOpen;
    [SerializeField] GameObject firstSelectedOnMenu;
    [SerializeField] GameObject firstSelectedOnControls;


    float sfxVolume;
    float musicVolume;

    private bool idiomaActivado = false;
    private int idiomaActual = 0;


    private void Start()
    {
        musicSlider.value = Settings.instance.GetMusicVolume();
        SFXSlider.value = Settings.instance.GetSFXVolume();

    }
    public void SettingsButton()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelectedOnMenu);

    }
    public void ShowControls()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        controlsPanel.SetActive(true);
        keyboardControls.SetActive(true);
        ControllerControls.SetActive(false);
        generalSettings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelectedOnControls);

    }
    public void BackToGame()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);
        optionsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        PlayerStats.instance.menuOpened = false;
        Time.timeScale = 1f;
    }
    public void CloseOptions()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelectedOnOpen);

    }
    public void CloseControls()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

        controlsPanel.SetActive(false);
        generalSettings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelectedOnMenu);

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
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
    private IEnumerator SetIdLocal(int localId)
    {
        idiomaActivado = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localId];
        PlayerPrefs.SetInt("LocaleKey", localId);
        PlayerPrefs.Save();
        idiomaActivado = false;
    }
   
}
