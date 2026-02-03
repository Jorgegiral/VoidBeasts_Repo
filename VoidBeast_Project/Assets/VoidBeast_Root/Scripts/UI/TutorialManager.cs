using System;
using System.Collections;
using UnityEngine;
using static TutorialManager;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public Dialogue dialogue;
    //public int step = 0;
    public Step currentStep = Step.Movement;
    public bool IsAnyStep(params Step[] steps)
    {
        foreach (Step step in steps)
        {
            if (currentStep == step)
                return true;
        }
        return false;
    }
    public enum Step
    {
        Movement = 0,
        OpenBuildMenu = 1,
        BuyPlot = 2,
        PlacePlot = 3,
        Cancel = 4,
        CloseBuild = 5,
        Interact = 6,
        Notification = 7,
        PlantingExplanation = 8,
        Plant = 9,
        ExitPlanting = 10,
        Night = 11,
        KillEnemies = 12,
        Collection = 13,
        Upgrade = 14,
        Money = 15,
        Upgrades = 16,
        Final = 17
    }
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrow1;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private GameObject arrow3;
    [SerializeField] private GameObject arrow4;
    [SerializeField] private GameObject bloqueo;
    [SerializeField] private GameObject bloqueo2;
    [SerializeField] private GameObject bloqueoN;
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject black2;
    [SerializeField] private GameObject dialoguetext;
    [SerializeField] private GameObject buildBlock;
    [SerializeField] private GameObject seedBlock;
    [SerializeField] private GameObject seedUnblock;
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
        switch (currentStep)
        {
            case Step.Movement: // moverse
                dialogue.WaitForAction();
                bloqueoN.gameObject.SetActive(true);
                break;

            case Step.OpenBuildMenu: // TAB construcción
                dialogue.WaitForAction();
                break;

            case Step.BuyPlot: //Comprar parcela
                dialogue.WaitForAction();
                buildmode.SetAsLastSibling();
                black2.SetActive(true);
                buildBlock.SetActive(true);
                bloqueo2.SetActive(true);
                arrow.SetActive(true);
                break;

            case Step.PlacePlot: // Poner parcela
                dialogue.WaitForAction();
                buildBlock.SetActive(false);
                black2.SetActive(false);
                arrow.SetActive(false);
                break;

            case Step.Cancel: // Cancelar
                dialogue.WaitForAction();
                buildmode.SetSiblingIndex(4);
                black2.SetActive(true);
                break;

            case Step.CloseBuild: // Tab otra vez
                dialogue.WaitForAction();
                black2.SetActive(false);
                break;

            case Step.Interact: //  Interactuar
                dialogue.WaitForAction();
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case Step.Notification: // Notificacion
                dialogue.WaitForAction();
                black.SetActive(true);
                blackR.SetSiblingIndex(4);
                bloqueo.SetActive(true);
                rectTransform.offsetMin = new Vector2(1000f, rectTransform.offsetMin.y);
                arrow2.SetActive(true);
                break;

            case Step.PlantingExplanation: //explicación plantar
                dialogue.WaitForAction();
                seedmode.SetAsLastSibling();
                seedBlock.SetActive(true);
                black.SetActive(false);
                black2.SetActive(true);
                arrow2.SetActive(false);
                break;

            case Step.Plant: // Plantar
                dialogue.WaitForAction();
                bloqueo.SetActive(false);
                bloqueo2.SetActive(false);
                seedUnblock.SetActive(false);
                arrow1.SetActive(true);
                break;

            case Step.ExitPlanting: // Salir de plantar
                dialogue.WaitForAction();
                black2.SetActive(false);
                seedBlock.SetActive(false);
                seedmode.SetSiblingIndex(3);
                arrow1.SetActive(false);
                break;

            case Step.Night: // Noche
                dialogue.WaitForAction();
                bloqueoN.gameObject.SetActive(false);
                arrow3.SetActive(true);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case Step.KillEnemies: // Mata los enemigos
                dialogue.WaitForAction();
                arrow3.SetActive(false);
                break;

            case Step.Collection: // recoleccion
                dialogue.WaitForAction();
                close.SetActive(true);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                break;

            case Step.Upgrade: // Mejora
                dialogue.WaitForAction();
                close.SetActive(false);
                rectTransform.offsetMin = new Vector2(596f, rectTransform.offsetMin.y);
                break;

            case Step.Money: // dinero
                dialogue.WaitForAction();
                break;

            case Step.Upgrades: // Mejoras
                dialogue.WaitForAction();
                arrow4.SetActive(true);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                rectTransform.offsetMax = new Vector2(-450f, rectTransform.offsetMax.y);
                break;

            case Step.Final:
                dialogue.WaitForAction();
                arrow4.SetActive(false);
                rectTransform.offsetMax = new Vector2(-215f, rectTransform.offsetMax.y);
                break;
        }
    }

    private void Update()
    {
        EscapeTecle();
    }
    public void CompleteStep()
    {
        int nextStepValue = (int)currentStep + 1;
        if (Enum.IsDefined(typeof(Step), nextStepValue))
        {
            currentStep = (Step)nextStepValue;
        }
        dialogue.ForceNextText();
        StartStep();
    }

    public void OnTutorialButtonPressed()
    {
        if (currentStep != Step.BuyPlot) return;
        CompleteStep();
    }

    public void OnNightButtonPressed()
    {
        if (currentStep != Step.Night) return;
        CompleteStep();
    }

    public void EscapeTecle()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentStep == Step.Cancel)
        {
            CompleteStep();
            return;
        }
    }
}
