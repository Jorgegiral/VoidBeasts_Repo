using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private string[] text;
    public string[] textEnglish;
    public string[] textSpanish;
    public string[] textCatalan;
    private int currentLanguageId = 0;
    public float textSpeed = 0.2f;
    public int index;
    public bool waitForAction = false;
    IEnumerator Start()
    {
        yield return UnityEngine.Localization.Settings.LocalizationSettings.InitializationOperation;

        GetCurrentLanguageIdFromLocalization();
        SelectLanguageArray();

        dialogueText.text = string.Empty;
        StartDialogue();
    }


    private void GetCurrentLanguageIdFromLocalization()
    {
        var locale = UnityEngine.Localization.Settings.LocalizationSettings.SelectedLocale;

        if (locale == null)
        {
            currentLanguageId = 0;
            return;
        }

        if (locale.Identifier.Code == "en")
            currentLanguageId = 0;
        else if (locale.Identifier.Code == "es")
            currentLanguageId = 1;
        else if (locale.Identifier.Code == "ca")
            currentLanguageId = 2;
        else
            currentLanguageId = 0;

    }
    private void SelectLanguageArray()
    {
        switch (currentLanguageId)
        {
            case 0: // Inglés
                text = textEnglish;
                Debug.Log("Idioma: Inglés");
                break;

            case 1: // Español
                text = textSpanish;
                Debug.Log("Idioma: Español");
                break;


            case 2: // Catalán
                text = textCatalan;
                Debug.Log("Idioma: Catalán");
                break;

            default: 
                text = textEnglish;
                Debug.Log("Idioma por defecto: Español");
                break;
        }

        if (text == null || text.Length == 0)
        {
            text = textSpanish;
        }
    }

    public void UpdateLanguage(int newLanguageId)
    {
        currentLanguageId = newLanguageId;
        SelectLanguageArray();

        // Si hay un diálogo mostrándose, actualizarlo
        if (dialogueText != null && index < text.Length)
        {
            StopAllCoroutines();
            dialogueText.text = text[index];
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text != text[index])
            {
                StopAllCoroutines();
                dialogueText.text = text[index];
                return;
            }
            if (TutorialManager.instance.IsAnyStep(TutorialManager.Step.Notification,
                TutorialManager.Step.PlantingExplanation, TutorialManager.Step.Money, TutorialManager.Step.Upgrades,
                TutorialManager.Step.Final))
            {
                TutorialManager.instance.CompleteStep();
                return;
            }
            if (waitForAction) return;

            NextText();
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(Dialogues());
    }

    IEnumerator Dialogues()
    {
        string richText = text[index];
        string plainText = RemoveRichTextTags(richText);

        dialogueText.text = "";

        foreach (char letter in plainText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        dialogueText.text = richText;
    }
    private string RemoveRichTextTags(string input)
    {
        string result = input;

        result = System.Text.RegularExpressions.Regex.Replace(result, "<b>|</b>", "");
        result = System.Text.RegularExpressions.Regex.Replace(result, "<i>|</i>", "");
        result = System.Text.RegularExpressions.Regex.Replace(result, "<color=.*?>|</color>", "");
        result = System.Text.RegularExpressions.Regex.Replace(result, "<size=.*?>|</size>", "");
        result = System.Text.RegularExpressions.Regex.Replace(result, "<.*?>", "");

        return result;
    }

    public void NextText()
    {
        if (index < text.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(Dialogues());
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void ForceNextText()
    {
        StopAllCoroutines();
        dialogueText.text = text[index];
        NextText();
    }
    public void WaitForAction()
    {
        waitForAction = true;
    }
    public int GetCurrentIndex()
    {
        return index;
    }
}
