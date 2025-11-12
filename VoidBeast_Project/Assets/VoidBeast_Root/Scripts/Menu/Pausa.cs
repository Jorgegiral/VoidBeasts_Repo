using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject generalSettings;
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject ControllerControls;

    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioClip unClickSound;


    private bool idiomaActivado = false;
    private int idiomaActual = 0;


    public void ExitButton()
    {
        generalSettings.SetActive(false);
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

    }
    public void SettingsButton()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);
        generalSettings.SetActive(true);



    }


    public void ShowControls()
    {
        Settings.instance.PlaySoundFXClip(ClickSound, transform, 1f);

        controlsPanel.SetActive(true);
        keyboardControls.SetActive(true);
        ControllerControls.SetActive(false);
        generalSettings.SetActive(false);
    }
    public void CloseControls()
    {
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

        controlsPanel.SetActive(false);
        generalSettings.SetActive(true);
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

}
