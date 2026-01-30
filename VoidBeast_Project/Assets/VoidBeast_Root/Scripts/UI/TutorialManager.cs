using System;
using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public Dialogue dialogue;
    public int step = 0;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrow1;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private GameObject arrow3;
    [SerializeField] private GameObject arrow4;
    [SerializeField] private GameObject bloqueo;
    [SerializeField] private GameObject bloqueo2;
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject black2;
    [SerializeField] private GameObject dialoguetext;
    [SerializeField] private GameObject buildBlock;
    [SerializeField] private GameObject seedBlock;
    [SerializeField] private RectTransform seedmode;
    [SerializeField] private RectTransform buildmode;
    private RectTransform rectTransform;
    private RectTransform blackR;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartStep();
        rectTransform = dialoguetext.GetComponent<RectTransform>();
        blackR = black.GetComponent<RectTransform>();
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
                buildmode.SetAsLastSibling();
                black2.SetActive(true);
                buildBlock.SetActive(true);
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                bloqueo2.SetActive(true);
                arrow.SetActive(true);
                break;

            case 3: // Poner parcela
                dialogue.WaitForAction();
                black2.SetActive(false);
                arrow.SetActive(false);
                break;

            case 4: // Cancelar
                dialogue.WaitForAction();
                buildmode.SetSiblingIndex(4);
                black2.SetActive(true);
                break;

            case 5: // Tab otra vez
                dialogue.WaitForAction();
                black2.SetActive(false);
                break;

            case 6: //  Interactuar
                dialogue.WaitForAction();
                buildBlock.SetActive(false);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case 7: // Notificacion
                dialogue.WaitForAction();
                black.SetActive(true);
                blackR.SetSiblingIndex(5);
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                arrow2.SetActive(true);
                break;

            case 8: //explicación plantar
                dialogue.WaitForAction();
                seedmode.SetAsLastSibling();
                seedBlock.SetActive(true);
                black.SetActive(false);
                black2.SetActive(true);
                arrow2.SetActive(false);
                bloqueo.SetActive(true);
                break;

            case 9: // Plantar
                dialogue.WaitForAction();
                seedBlock.SetActive(false);
                arrow1.SetActive(true);
                bloqueo.SetActive(false);
                break;

            case 10: // Salir de plantar
                dialogue.WaitForAction();
                black2.SetActive(false);
                seedmode.SetSiblingIndex(3);
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
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;
        }
    }

    private void Update()
    {
        EscapeTecle();
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

    public void EscapeTecle()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && step != 4)
        {
            CompleteStep();
            return;
        }
    }
}
