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
    public string[] textSpanish;
    public string[] textEnglish;
    public string[] textCatalan;
    private int currentLanguageId = 0;
    public float textSpeed = 0.2f;
    public int index;
    public bool waitForAction = false;
    private Coroutine typingCoroutine;
    [SerializeField] private RectTransform backgroundPanel;
    [SerializeField] private ContentSizeFitter sizeFitter;
    void Start()
    {
        GetCurrentLanguageId();
        SelectLanguageArray(); 
        dialogueText.text = string.Empty;
        StartDialogue();
    }
    private void GetCurrentLanguageId()
    {
        if (PlayerPrefs.HasKey("LocaleKey"))
        {
            currentLanguageId = PlayerPrefs.GetInt("LocaleKey");
        }
        else
        {
            currentLanguageId = 0;
        }

        Debug.Log($"Idioma ID: {currentLanguageId}");
    }
    private void SelectLanguageArray()
    {
        switch (currentLanguageId)
        {
            case 0: // Español
                text = textSpanish;
                Debug.Log("Idioma: Español");
                break;

            case 1: // Inglés
                text = textEnglish;
                Debug.Log("Idioma: Inglés");
                break;

            case 2: // Catalán
                text = textCatalan;
                Debug.Log("Idioma: Catalán");
                break;

            default: // Por defecto español
                text = textSpanish;
                Debug.Log("Idioma por defecto: Español");
                break;
        }

        // Verificar que el array seleccionado tenga contenido
        if (text == null || text.Length == 0)
        {
            Debug.LogWarning("Array de textos vacío. Usando español por defecto.");
            text = textSpanish;
        }
    }

    private string DetectSystemLanguage()
    {
        SystemLanguage sysLang = Application.systemLanguage;

        switch (sysLang)
        {
            case SystemLanguage.Spanish:
                return "es";

            case SystemLanguage.Catalan:
                return "ca";

            case SystemLanguage.English:
                return "en";

            default:
                return "es";
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
