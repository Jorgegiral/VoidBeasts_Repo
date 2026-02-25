using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
        CloseBuild = 4,
        Interact = 5,
        Notification = 6,
        PlantingExplanation = 7,
        Plant = 8,
        ExitPlanting = 9,
        Night = 10,
        KillEnemies = 11,
        Collection = 12,
        Upgrade = 13,
        Money = 14,
        Upgrades = 15,
        Final = 16
    }
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrow1;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private GameObject arrow3;
    [SerializeField] private GameObject arrow4;
    [SerializeField] private GameObject arrow5;
    [SerializeField] private GameObject arrow6;
    [SerializeField] private GameObject bloqueo;
    [SerializeField] private GameObject bloqueo2;
    [SerializeField] private GameObject bloqueoN;
    [SerializeField] private GameObject bloqueoBotones;
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject black2;
    [SerializeField] private GameObject dialoguetext;
    [SerializeField] private GameObject buildBlock;
    [SerializeField] private GameObject seedBlock;
    [SerializeField] private GameObject seedUnblock;
    [SerializeField] private GameObject robot;
    [SerializeField] private RectTransform seedmode;
    [SerializeField] private RectTransform buildmode;
    [SerializeField] private RectTransform money;
    [SerializeField] private Sprite[] robotS;
    private RectTransform rectTransform;
    private RectTransform blackR;
    private RectTransform robotR;
    private Animator ranim;
    private Image robotI;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartStep();
        rectTransform = dialoguetext.GetComponent<RectTransform>();
        robotR = robot.GetComponent<RectTransform>();
        blackR = black.GetComponent<RectTransform>();
        ranim = robot.GetComponent<Animator>();
        robotI = robot.GetComponent<Image>();
    }
    void StartStep()
    {
        switch (currentStep)
        {
            case Step.Movement: // moverse
                dialogue.WaitForAction();
                bloqueoN.gameObject.SetActive(true);
                bloqueoBotones.gameObject.SetActive(true);
                break;

            case Step.OpenBuildMenu: // TAB construcción
                arrow6.SetActive(true);
                dialogue.WaitForAction();
                rectTransform.offsetMax = new Vector2(-450f, rectTransform.offsetMax.y);
                robotR.anchoredPosition = new Vector2(500f, rectTransform.anchoredPosition.y);
                robotI.sprite = robotS[1];
                break;

            case Step.BuyPlot: //Comprar parcela
                dialogue.WaitForAction();
                buildmode.SetAsLastSibling();
                black2.SetActive(true);
                buildBlock.SetActive(true);
                bloqueo2.SetActive(true);
                arrow.SetActive(true);
                arrow6.SetActive(false);
                break;

            case Step.PlacePlot: // Poner parcela
                dialogue.WaitForAction();
                buildBlock.SetActive(false);
                black2.SetActive(false);
                arrow.SetActive(false);
                robotI.sprite = robotS[0];
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
                rectTransform.offsetMin = new Vector2(800f, rectTransform.offsetMin.y);
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
                black.SetActive(true);
                blackR.SetSiblingIndex(5);
                money.SetAsLastSibling();
                arrow5.SetActive(true);
                break;

            case Step.Upgrades: // Mejoras
                dialogue.WaitForAction();
                arrow4.SetActive(true);
                arrow5.SetActive(false);
                rectTransform.offsetMin = new Vector2(880f, rectTransform.offsetMin.y);
                rectTransform.offsetMax = new Vector2(-450f, rectTransform.offsetMax.y);
                break;

            case Step.Final:
                dialogue.WaitForAction();
                black.SetActive(false);
                arrow4.SetActive(false);
                rectTransform.offsetMax = new Vector2(-215f, rectTransform.offsetMax.y);
                break;
        }
    }

    public void CompleteStep()
    {
        int nextStepValue = (int)currentStep + 1;
        if (Enum.IsDefined(typeof(Step), nextStepValue))
        {
            currentStep = (Step)nextStepValue;
        }
        dialogue.ForceNextText();
        ranim.SetTrigger("Change");
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
}
