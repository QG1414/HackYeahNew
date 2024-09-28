using SteelLotus.Core.Events;
using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundsGenerator : MonoBehaviour
{
    [SerializeField]
    private Vector2 soundFrequency;

    private int currentNumerator = 0;

    private void Awake()
    {
        GameEvents.Instance.OnSecondUpdate += UpdateMethod;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnSecondUpdate -= UpdateMethod;
    }

    private void UpdateMethod()
    {
        int value = Mathf.FloorToInt(Random.Range(soundFrequency.x, soundFrequency.y));

        if(currentNumerator >= value)
        {
            currentNumerator = 0;
            AudioClip clip = SoundManager.Instance.AmbientCollection.clips[Random.Range(0, SoundManager.Instance.AmbientCollection.clips.Count)];
            SoundManager.Instance.PlayOneShoot(SoundManager.Instance.AmbientSource, clip);
        }

        currentNumerator += 1;
    }
}
