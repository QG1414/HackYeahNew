using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackingNotification : MonoBehaviour
{
    [SerializeField]
    private float effectTime;

    [SerializeField]
    private Image warningSign;

    [SerializeField]
    private TimeVisualization timeVisualization;

    private Coroutine effectCoroutine;

    private void Awake()
    {
        GameEvents.Instance.OnEventStarted += StartEffect;
        GameEvents.Instance.OnEventStopped += StopEffect;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnEventStarted -= StartEffect;
        GameEvents.Instance.OnEventStopped -= StopEffect;
    }

    public void StartEffect(HackingEvent _)
    {
        Debug.LogError("start effect");
        warningSign.color = new Color(1, 1, 1, 1);
        warningSign.gameObject.SetActive(true);
        timeVisualization.DisableText();
        effectCoroutine = StartCoroutine(EffectActive());
    }

    public void StopEffect(bool _)
    {
        if(effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
        }

        warningSign.gameObject.SetActive(false);
        timeVisualization.EnableText();
    }

    private IEnumerator EffectActive()
    {
        float timer = 0;
        while(true)
        {
            timer = 0;
            Color startColor = warningSign.color;
            while (warningSign.color.r > 0.5f)
            {
                warningSign.color = Color.Lerp(startColor, new Color(0.5f,0.5f,0.5f,1), timer / effectTime);
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0;
            startColor = warningSign.color;

            while (warningSign.color.r < 1f)
            {
                warningSign.color = Color.Lerp(startColor, new Color(1f,1f, 1f, 1), timer / effectTime);
                timer += Time.deltaTime;
                yield return null;
            }

            yield return null;

        }
    }
}
