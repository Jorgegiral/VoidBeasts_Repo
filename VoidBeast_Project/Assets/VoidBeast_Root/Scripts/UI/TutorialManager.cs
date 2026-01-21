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
    [SerializeField] private GameObject dialoguetext;
    private RectTransform rectTransform;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartStep();
        rectTransform = dialoguetext.GetComponent<RectTransform>();
    }
    void StartStep()
    {
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
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                arrow.SetActive(true);
                break;

            case 3: // Poner parcela
                dialogue.WaitForAction();
                arrow.SetActive(false);
                break;

            case 4: // Cancelar
                dialogue.WaitForAction();
                break;

            case 5: // Tab otra vez
                dialogue.WaitForAction();
                break;

            case 6: //  Interactuar
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 7: // Plantar 
                dialogue.WaitForAction();
                arrow1.SetActive(true);
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                break;

            case 8: // salir de plantar
                dialogue.WaitForAction();
                arrow1.SetActive(false);
                break;

            case 9: // Banco
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 10: // Mata los enemigos
                dialogue.WaitForAction();
                break;

            case 11: // Mejora
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(1250f, rectTransform.offsetMin.y);
                break;

            case 12: // recoleccion
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;
        }
    }
    public void CompleteStep()
    {
        step++;
        dialogue.ForceNextText();
        StartStep();
    }

    public void OnTutorialButtonPressed()
    {
        if (step != 2) return;
        CompleteStep();
    }
    
}
