    using System.Collections;
    using Unity.Cinemachine;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class ToggleConstruction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] GameObject constructionShop;
        [SerializeField] GameObject grid;
        [SerializeField] GameObject playerCam;
        [SerializeField] GameObject buildCam;
        [SerializeField] GameObject tableRun;
        [SerializeField] GameObject tableIdle;

        public PlayerInput playerInput;

        [Header("Animation References")]
        private Animator anim;

        //Jorge:
        bool tutorialTabDone = false;
        bool tutorialTabClose = false;
        private void Start()
        {
            anim = GetComponent<Animator>();
            StartCoroutine(DisableGridDelayed());
        }
        public void TableActived()
        {
            tableRun.SetActive(true);
            tableIdle.SetActive(false);

        }

        public void TableDesactived()
        {
            tableRun.SetActive(false);

        }
        public void TableIdleActivate()
        {
            tableIdle.SetActive(true);
            tableRun.SetActive(false);

        }
        public void TableIdleDesactivated()
        {
            tableIdle.SetActive(false);

        }
        public void SwitchMode(InputAction.CallbackContext context)
        {
            if (TutorialManager.instance != null && TutorialManager.instance.currentStep == TutorialManager.Step.OpenBuildMenu)
            {
                if (TutorialManager.instance != null && TutorialManager.instance.step == 1)
                {
                    if (!tutorialTabDone)
                    {
                        tutorialTabDone = true;
                        TutorialManager.instance.CompleteStep();
                    }
                }
            }
            if (TutorialManager.instance != null && TutorialManager.instance.currentStep == TutorialManager.Step.CloseBuild)
            {
                if (!tutorialTabClose)
                {
                    if (!tutorialTabClose)
                    {
                        tutorialTabClose = true;
                        TutorialManager.instance.CompleteStep();
                    }
                }
            }

        if (TutorialManager.instance == null || TutorialManager.instance.currentStep >= TutorialManager.Step.BuyPlot)
        {
            
                if (PlayerStats.instance.isActionMode && DayNightSystem.Instance.isDay)
            {
                MoneySystem.instance.UpdateMoneyText();
                playerInput.SwitchCurrentActionMap("BuildMode");
                constructionShop.SetActive(true);
                playerCam.SetActive(false);
                buildCam.SetActive(true);
                grid.SetActive(true);
                anim.SetBool("isBuilding", true);

                PlayerStats.instance.isActionMode = false;
            }
            else if (!PlayerStats.instance.isActionMode && DayNightSystem.Instance.isDay && GridBuilding.instance.buildingTemp == null)
            {
                MoneySystem.instance.UpdateMoneyText();

                playerInput.SwitchCurrentActionMap("ActionMode");
                constructionShop.SetActive(false);
                playerCam.SetActive(true);
                buildCam.SetActive(false);
                grid.SetActive(false);
                anim.SetBool("isBuilding", false);
                PlayerStats.instance.isActionMode = true;

            }

        }
        }
        private IEnumerator DisableGridDelayed()
        {
            yield return new WaitForSeconds(0.1f);
            grid.SetActive(false);
        }
    }

