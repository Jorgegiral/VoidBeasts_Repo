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
    public GameObject arrow3;
    public GameObject arrow4;
    public GameObject bloqueo;
    public GameObject bloqueo2;
    public GameObject close;
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
                bloqueo2.SetActive(true);
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

            case 7: // Notificacion
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                arrow2.SetActive(true);
                break;

            case 8: //explicación plantar
                dialogue.WaitForAction();
                arrow2.SetActive(false);
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
                bloqueo2.SetActive(false);
                arrow3.SetActive(true);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 12: // Mata los enemigos
                dialogue.WaitForAction();
                arrow3.SetActive(false);
                break;

            case 13: // Mejora
                dialogue.WaitForAction();
                close.SetActive(false);
                rectTransform.offsetMin = new Vector2(596f, rectTransform.offsetMin.y);
                break;

            case 14: // recoleccion
                dialogue.WaitForAction();
                close.SetActive(true);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 15: // dinero
                dialogue.WaitForAction();
                break;

            case 16: // Mejoras
                dialogue.WaitForAction();
                arrow4.SetActive(true);
                rectTransform.offsetMin = new Vector2(880f, 490f);
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
