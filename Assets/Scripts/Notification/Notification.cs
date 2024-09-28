using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup mainCanvas;

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float letterDuration;

    [SerializeField]
    private float screenDuration;


    private Coroutine dialogueCoroutine = null;
    private string savedDialogue = "";


    public void StartDialogue()
    {
        ClearText();
        StartCoroutine(DisplayScreen(true));
    }

    public void ChangeText(string dialogue)
    {
        ClearText();
        savedDialogue = dialogue;

        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        dialogueCoroutine = StartCoroutine(DisplayText(dialogue));
    }

    private void ClearText()
    {
        text.text = "";
    }

    public bool SkipDialogue()
    {
        if(dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
            text.text = savedDialogue;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopDialogue()
    {
        ClearText();
        StartCoroutine(DisplayScreen(false));
    }





    private IEnumerator DisplayScreen(bool on)
    {
        float value = 0;
        float timeElapsed = 0;

        if (on)
        {
            while (value < 100)
            {
                value = Mathf.Lerp(0, 100, timeElapsed / screenDuration);
                timeElapsed += Time.deltaTime;

                mainCanvas.alpha = value;
                yield return null;
            }
        }
        else
        {
            mainCanvas.alpha = 0;
            yield break;
        }
    }

    private IEnumerator DisplayText(string allLetters)
    {
        foreach(char letter in allLetters)
        {
            text.text += letter;
            yield return new WaitForSeconds(letterDuration);
        }

        dialogueCoroutine = null;
    }


}
