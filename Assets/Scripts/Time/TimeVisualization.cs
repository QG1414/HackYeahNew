using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeVisualization : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerPanel;

    TimeController timeController;


    private void Awake()
    {
        timeController = MainGameController.Instance.GetPropertyByType<TimeController>();
        GameEvents.Instance.OnSecondUpdate += SecondUpdate;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnSecondUpdate -= SecondUpdate;
    }

    private void SecondUpdate()
    {
        timerPanel.text = (timeController.CurrentTime).ToString();
    }

    public void DisableText()
    {
        timerPanel.gameObject.SetActive(false);
    }

    public void EnableText()
    {
        timerPanel.gameObject.SetActive(true);
    }
}
