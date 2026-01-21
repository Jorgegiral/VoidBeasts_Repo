using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] text;
    public float textSpeed = 0.1f;
    public int index;
    public bool waitForAction = false;
    public bool canAdvanceByClick = true;
    //public bool allowClick = true;

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

            if (waitForAction) return;
            if (!canAdvanceByClick) return;

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
            yield return new WaitForSeconds(textSpeed);
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
        canAdvanceByClick = false;
        waitForAction = true;
    }
    public void AllowClickAdvance()
    {
        canAdvanceByClick = true;
        waitForAction = false;
    }
    public int GetCurrentIndex()
    {
        return index;
    }
}
