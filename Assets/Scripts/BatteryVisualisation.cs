using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryVisualisation : MonoBehaviour
{
    [SerializeField]
    private Image batteryFill;

    private void Awake()
    {
        GameEvents.Instance.OnGeneratorChange += UpdateVisuals;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGeneratorChange -= UpdateVisuals;
    }

    void UpdateVisuals(float current, float max)
    {
        float fillValue = current / max;
        batteryFill.fillAmount = fillValue;
        batteryFill.color = new Color(1 - fillValue, fillValue, 0);
    }
}
