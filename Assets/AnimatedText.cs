using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimatedText : MonoBehaviour
{
    [SerializeField]
    private string text;

    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private float letterDuration;

    private void Awake()
    {
        textField.text = "";
    }

    public void StartAnimation()
    {
        textField.text = "";
        StartCoroutine(TextAnimation());
    }
    
    private IEnumerator TextAnimation()
    {
        foreach(char letter in text)
        {
            textField.text += letter;
            yield return new WaitForSeconds(letterDuration);
        }
    }
}
