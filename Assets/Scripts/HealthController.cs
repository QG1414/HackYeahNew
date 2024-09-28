using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> spritesLevels = new List<Sprite>();

    [SerializeField]
    private Image healthSprite;

    private int numberOfLives;

    private void Awake()
    {
        numberOfLives = spritesLevels.Count - 1;
    }

    public void UpdateHealth()
    {
        numberOfLives -= 1;
        healthSprite.sprite = spritesLevels[spritesLevels.Count - 1 - numberOfLives];

        if (numberOfLives <= 0)
        {
            GameEvents.Instance.CallOnGameOver();
        }
    }
}
