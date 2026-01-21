using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public Dialogue dialogue;
    public int step = 0;

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
                dialogue.AllowClickAdvance();
                break;
            case 2:// TAB construcción 2
                dialogue.WaitForAction();
                break;

            case 3: //Comprar parcela
                dialogue.WaitForAction();
                break;

            case 4: // Arrastrar parcela
                dialogue.WaitForAction();
                break;

            case 5: // Tab otra vez
                dialogue.WaitForAction();
                break;

            case 6: // Interactuar
                dialogue.WaitForAction();
                break;

            case 7: // Plantar
                dialogue.WaitForAction();
                break;

            case 8: // Sin dinero
                dialogue.AllowClickAdvance();
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
