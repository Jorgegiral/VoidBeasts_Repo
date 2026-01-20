using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] text;
    public float textSpeed = 0.1f;
    int index;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == text[index])
            {
                NextText();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = text[index];
            }
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
}
