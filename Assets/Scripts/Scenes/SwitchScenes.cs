using SteelLotus.Core;
using System;
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

    [SerializeField]
    private Vector2 startPosition;

    public void MoveToCenter()
    {
        screenBlocker.anchoredPosition = startPosition - positionChange;
    }

    [ContextMenu("start effect")]
    public float StartMovement(Action actionToPerforem = null)
    {
        AdditionalFunctions.Instance.ObjectMovement(true, screenBlocker, positionChange,startPosition,movementDuration, actionToPerforem);
        return movementDuration;
    }

    [ContextMenu("stop effect")]
    public float StopMovement(Action actionToPerforem = null)
    {
        AdditionalFunctions.Instance.ObjectMovement(false, screenBlocker, positionChange, startPosition, movementDuration, actionToPerforem);
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
