using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GeneratorMinigame : Minigame
{
    [SerializeField]
    private Image fill;

    [SerializeField]
    private float stepStrength;

    [SerializeField]
    private float decreaseStrength;

    private float value = 0;

    public override void StartMinigame()
    {
        base.StartMinigame();
        value = 0;
    }

    public void FillButton()
    {
        value += stepStrength;
    }

    private void Update()
    {
        if (!minigameActive)
            return;

        value -= decreaseStrength * Time.deltaTime;
        fill.fillAmount = Mathf.Clamp(value / 100f, 0, 1);

        if(value >= 100)
        {
            succeed = true;
            EndMinigame();
        }
    }
}
