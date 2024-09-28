using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalFunctions : Singleton<AdditionalFunctions>
{
    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void ObjectMovement(bool into, RectTransform transform, Vector2 positionChange, Vector2 startPosition, float movementDuration, Action invokeAfter = null)
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(Loading(into, transform, positionChange, startPosition, movementDuration, invokeAfter));
    }

    private IEnumerator Loading(bool into, RectTransform transform, Vector2 positionChange, Vector2 startPosition, float movementDuration, Action invokeAfter = null)
    {
        Vector2 startMovePosition = transform.anchoredPosition;
        float time = 0;
        Vector2 finalPosition = startMovePosition - positionChange;

        while (transform.anchoredPosition != finalPosition)
        {
            Vector2 newPosition = Vector2.Lerp(startMovePosition, finalPosition, time / movementDuration);
            transform.anchoredPosition = newPosition;
            time += Time.deltaTime;
            yield return null;
        }

        if (!into)
        {
            transform.anchoredPosition = startPosition;
        }

        if (invokeAfter != null)
        {
            invokeAfter.Invoke();
        }

        coroutine = null;
    }
}
