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


    private void Start()
    {
        timeController = MainGameController.Instance.GetPropertyByType<TimeController>();
        GameEvents.Instance.OnSecondUpdate += SecondUpdate;

        int hours = timeController.CurrentTime / 60;
        int minutes = timeController.CurrentTime % 60;

        timerPanel.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnSecondUpdate -= SecondUpdate;
    }

    private void SecondUpdate()
    {
        int hours = timeController.CurrentTime / 60;
        int minutes = timeController.CurrentTime % 60;
        timerPanel.text = string.Format("{0:00}:{1:00}", hours, minutes);
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
