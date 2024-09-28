using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private List<Image> spritesLevels = new List<Image>();

    [SerializeField]
    private Sprite healthSprite;

    [SerializeField]
    private Sprite damagedSprite;



    private int numberOfLives;

    private void Awake()
    {
        numberOfLives = spritesLevels.Count;

        foreach(Image image in spritesLevels)
        {
            image.sprite = healthSprite;
        }
    }

    public void UpdateHealth()
    {
        numberOfLives -= 1;
        spritesLevels[numberOfLives].sprite = damagedSprite;

        if (numberOfLives <= 0)
        {
            GameEvents.Instance.CallOnGameOver();
        }
    }
}
