using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private Image lamp;

    [SerializeField]
    private Vector2 timingEffect;

    [SerializeField]
    private int maxRandomBlinks;

    [SerializeField]
    private float shortBlinkInterval;

    [SerializeField]
    private Color baseColor;

    [SerializeField]
    private Color flickerColor;

    private Coroutine lampCoroutine = null;

    private float currentCounter = 0;

    private void Awake()
    {
        lampCoroutine = StartCoroutine(Blinking());
    }

    private void OnDestroy()
    {
        if(lampCoroutine != null)
        {
            StopCoroutine(lampCoroutine);
        }
    }

    private IEnumerator Blinking()
    {
        while(true)
        {
            currentCounter += Time.deltaTime;
            float effectShouldOccur = Random.Range(timingEffect.x, timingEffect.y);

            if(currentCounter >= effectShouldOccur)
            {
                currentCounter = 0;
                int numberOfBlinks = Random.Range(1, maxRandomBlinks);

                for(int i=0; i<numberOfBlinks; i++)
                {
                    lamp.color = flickerColor;
                    yield return new WaitForSeconds(shortBlinkInterval);
                    lamp.color = baseColor;
                    yield return new WaitForSeconds(shortBlinkInterval);
                }
            }
            yield return null;
        }
    }
}
