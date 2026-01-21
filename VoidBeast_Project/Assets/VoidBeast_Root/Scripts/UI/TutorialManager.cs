using System;
using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public Dialogue dialogue;
    public int step = 0;
    public GameObject arrow;
    public GameObject arrow1;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //dialogue.allowClick = false;
        StartStep();
    }

    void StartStep()
    {
        //dialogue.allowClick = true;
        switch (step)
        {
            case 0: // moverse
                dialogue.WaitForAction();
                break;

            case 1: // TAB construcción
                dialogue.WaitForAction();
                break;

            case 2: //Comprar parcela
                dialogue.WaitForAction();
                arrow.SetActive(true);
                break;

            case 3: // Arrastrar parcela
                dialogue.WaitForAction();
                arrow.SetActive(false);
                break;

            case 4: // Tab otra vez
                dialogue.WaitForAction();
                break;

            case 5: // Interactuar
                dialogue.WaitForAction();
                break;

            case 6: // Plantar  
                dialogue.WaitForAction();
                arrow1.SetActive(true);
                break;

            case 7: // salide de plantar
                dialogue.WaitForAction();
                arrow1.SetActive(false);
                break;

            case 8: // Banco
                dialogue.WaitForAction();
                break;

            case 9: // Mata los enemigos con CLICK IZQ
                dialogue.WaitForAction();
                break;
        }
    }
    public void CompleteStep()
    {
        step++;
        dialogue.ForceNextText();
        StartStep();
    }

    IEnumerator ExploreTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CompleteStep();
    }

    public void OnTutorialButtonPressed()
    {
        if (step != 2) return;

        CompleteStep();
    }
}
