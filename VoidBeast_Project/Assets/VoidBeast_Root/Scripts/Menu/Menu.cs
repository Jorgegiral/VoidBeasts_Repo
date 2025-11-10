using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Sprite backgroundDay;
    [SerializeField] Sprite backgroundNight;
    [SerializeField] GameObject logoImages;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject generalSettings;
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject ControllerControls;

    private bool idiomaActivado = false;
    private int idiomaActual = 0;


    private void Awake()
    {
        logoImages.SetActive(true);
        settingsPanel.SetActive(false);
        int numRandom = UnityEngine.Random.Range(0, 2);
        if (numRandom == 0) backgroundImage.sprite = backgroundNight;
        if (numRandom == 1) backgroundImage.sprite = backgroundDay;
        int Id = PlayerPrefs.GetInt("LocaleKey", 0);
    }

    public void ExitButton()
    {
       Application.Quit();
    }
    public void PlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void OptionsButton()
    {
        settingsPanel.SetActive(true);
        logoImages.SetActive(false);
    }
    public void CloseOptions()
    {
        settingsPanel.SetActive(false);
        logoImages.SetActive(true);
    }
    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        keyboardControls.SetActive(true);
        ControllerControls.SetActive(false);
        generalSettings.SetActive(false);
    }
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        generalSettings.SetActive(true);
    }
    public void ShowKeyboardControls()
    {
        keyboardControls.SetActive(true);
        ControllerControls.SetActive(false);
    }
    public void ShowControllerControls()
    {
        keyboardControls.SetActive(false);
        ControllerControls.SetActive(true);
    }

    public void CambiarIdiomas()
    {
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
}
