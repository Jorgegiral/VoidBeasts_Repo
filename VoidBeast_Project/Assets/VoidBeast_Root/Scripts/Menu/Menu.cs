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

    private void Awake()
    {
        int numRandom = UnityEngine.Random.Range(0, 2);
        if (numRandom == 0) backgroundImage.sprite = backgroundNight;
        if (numRandom == 1) backgroundImage.sprite = backgroundDay;
        Settings.Instance.menuSprite = backgroundImage.sprite;
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
        SceneManager.LoadScene(2);
    }
}
