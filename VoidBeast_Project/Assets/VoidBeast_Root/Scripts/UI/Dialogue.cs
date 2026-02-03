using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] text;
    public float textSpeed = 0.1f;
    public int index;
    public bool waitForAction = false;

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
            if (TutorialManager.instance.step == 4 || TutorialManager.instance.step == 7 || TutorialManager.instance.step == 8 || 
                TutorialManager.instance.step == 14 || TutorialManager.instance.step == 15 || TutorialManager.instance.step == 16)
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
        foreach (char letter in text[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
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
            gameObject.SetActive(false);
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
