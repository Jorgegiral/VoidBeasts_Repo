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
    public GameObject arrow2;
    public GameObject bloqueo;
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

            case 1: //notificación
                dialogue.WaitForAction();
                arrow2.SetActive(true);
                break;

            case 2: // TAB construcción
                dialogue.WaitForAction();
                arrow2.SetActive(false);
                break;

            case 3: //Comprar parcela
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                arrow.SetActive(true);
                break;

            case 4: // Poner parcela
                dialogue.WaitForAction();
                arrow.SetActive(false);
                break;

            case 5: // Cancelar
                dialogue.WaitForAction();
                break;

            case 6: // Tab otra vez
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 7: //  Interactuar
                dialogue.WaitForAction();
                break;

            case 8: //explicación plantar
                dialogue.WaitForAction();
                bloqueo.SetActive(true);
                break;

            case 9: // Plantar
                dialogue.WaitForAction();
                arrow1.SetActive(true);
                bloqueo.SetActive(false);
                break;

            case 10: // Salir de plantar
                dialogue.WaitForAction();
                arrow1.SetActive(false);
                break;

            case 11: // Banco
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                break;

            case 12: // Mata los enemigos
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 13: // Mejora
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(1250f, rectTransform.offsetMin.y);
                break;

            case 14: // recoleccion
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 15: // dinero
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

    public void OnTutorialButtonPressed()
    {
        if (step != 3) return;
        CompleteStep();
    }
    
}
