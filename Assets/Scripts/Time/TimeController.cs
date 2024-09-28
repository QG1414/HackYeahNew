using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField, Tooltip("add in seconds")]
    private int maxTime;

    private float currentTime;

    public int MaxTime { get => maxTime; }
    public float CurrentTime { get => currentTime; }


    private void Awake()
    {
        currentTime = maxTime;

        InvokeRepeating(nameof(UpdateMethod), 0, 1);
    }




    private void UpdateMethod()
    {
        if (!MainGameController.Instance.GameStarted)
            return;

        currentTime -= 1;
        GameEvents.Instance.CallOnSecondUpdate();


        if (currentTime <= 0)
        {
            MainGameController.Instance.GameStarted = false;
            GameEvents.Instance.CallOnGameEnd();
        }
    }

}
