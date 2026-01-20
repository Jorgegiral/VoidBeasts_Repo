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
        dialogue.allowClick = false;
        StartStep();
    }

    void StartStep()
    {
        switch (step)
        {
            case 0: // moverse
                //dialogue.WaitForAction();
                //CheckMovement();
                break;

            case 1: // TAB construcción
                //dialogue.WaitForAction();
                break;
            case 2:
                // "Ataca con click izquierdo"
                break;

            case 3: // interactuar con E
                //dialogue.WaitForAction();
                break;

            case 8: // atacar
                //dialogue.WaitForAction();
                break;
        }
    }
    public void CompleteStep()
    {
        step++;
        dialogue.ForceNextText();
        StartStep();
    }
    /*
    void CheckMovement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0)
        {
            dialogue.ContinueAfterAction();
        }
    }
    
    // Estos métodos los llamas desde el gameplay REAL
    public void OnBuildModeOpened()
    {
        if (dialogue.GetCurrentIndex() == 1)
            dialogue.ContinueAfterAction();
    }

    public void OnInteract()
    {
        if (dialogue.GetCurrentIndex() == 3)
            dialogue.ContinueAfterAction();
    }

    public void OnAttack()
    {
        if (dialogue.GetCurrentIndex() == 8)
            dialogue.ContinueAfterAction();
    }*/
}
