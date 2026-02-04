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
    public string[] text;
    public float textSpeed = 0.2f;
    public int index;
    public bool waitForAction = false;
    private Coroutine typingCoroutine;
    [SerializeField] private RectTransform backgroundPanel;
    [SerializeField] private ContentSizeFitter sizeFitter;
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
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

        /*dialogueText.text = text[index];
        dialogueText.ForceMeshUpdate();
        dialogueText.maxVisibleCharacters = 0;

        int totalCharacters = dialogueText.textInfo.characterCount;

        for (int visibleCount = 0; visibleCount <= totalCharacters; visibleCount++)
        {
            dialogueText.maxVisibleCharacters = visibleCount;
            yield return new WaitForSecondsRealtime(textSpeed);
        }

        typingCoroutine = null;*/

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

        // Eliminar tags específicos
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
