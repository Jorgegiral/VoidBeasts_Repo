using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Sprite backgroundDay;
    [SerializeField] Sprite backgroundNight;
    [SerializeField] GameObject logoImages;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject controlsPanel;

    private void Awake()
    {
        logoImages.SetActive(true);
        settingsPanel.SetActive(false);
        int numRandom = UnityEngine.Random.Range(0, 2);
        if (numRandom == 0) backgroundImage.sprite = backgroundNight;
        if (numRandom == 1) backgroundImage.sprite = backgroundDay;
    }

    public void ExitButton()
    {
       Application.Quit();
    }
    public void PlayButton()
    {
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
    }
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }
}
