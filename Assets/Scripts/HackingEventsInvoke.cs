using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEventsInvoke : MonoBehaviour
{
    [SerializeField]
    private Vector2 eventTimer;

    [SerializeField]
    private List<HackingEvent> hackingEvents = new List<HackingEvent>();

    private HackingEvent currentEvent;
    private MinigameController minigameController;

    int currentTime = 0;


    private void Awake()
    {
        if (minigameController == null)
            minigameController = MainGameController.Instance.GetPropertyByType<MinigameController>();

        GameEvents.Instance.OnEventStopped += EvenStopped;
        GameEvents.Instance.OnSecondUpdate += UpdateMethod;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnEventStopped -= EvenStopped;
        GameEvents.Instance.OnSecondUpdate -= UpdateMethod;
    }

    private void UpdateMethod()
    {
        int value = Mathf.FloorToInt(Random.Range(eventTimer.x, eventTimer.y));
        if (currentTime >= value && !minigameController.GeneratorMinigameActive() && !MainGameController.Instance.GeneratorCritical)
        {
            currentTime = 0;
            StartRandomEvent();
        }

        currentTime += 1;
    }

    private void StartRandomEvent()
    {
        HackingEvent currentEvent = hackingEvents[Random.Range(0, hackingEvents.Count)];
        this.currentEvent = currentEvent;
        MainGameController.Instance.GameStarted = false;
        currentEvent.StartEvent();
        GameEvents.Instance.CallOnEventStarted(currentEvent);
    }

    private void EvenStopped(bool win)
    {
        if(!win)
        {
            MainGameController.Instance.DecreaseHealth();
        }

        currentEvent = null;
        MainGameController.Instance.GameStarted = true;
    }


}
