using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScenes : MonoBehaviour
{
    [SerializeField]
    private RectTransform screenBlocker;

    [SerializeField]
    private float movementDuration;

    [SerializeField]
    private Vector2 positionChange;

    private Vector2 startPosition;

    private void Awake()
    {
        startPosition = screenBlocker.anchoredPosition;
    }


    [ContextMenu("start effect")]
    public float StartMovement()
    {
        StartCoroutine(Loading(true));
        return movementDuration;
    }

    [ContextMenu("stop effect")]
    public float StopMovement()
    {
        StartCoroutine(Loading(false));
        return movementDuration;
    }

    private IEnumerator Loading(bool into)
    {
        Vector2 startPositiomn = screenBlocker.anchoredPosition;
        float time = 0;
        Vector2 finalPosition = startPositiomn - positionChange;

        while(screenBlocker.anchoredPosition != finalPosition)
        {
            Vector2 newPosition = Vector2.Lerp(startPositiomn, finalPosition, time / movementDuration);
            screenBlocker.anchoredPosition = newPosition;
            time += Time.deltaTime;
            yield return null;
        }

        if(!into)
        {
            screenBlocker.anchoredPosition = startPosition;
        }
    }
}
