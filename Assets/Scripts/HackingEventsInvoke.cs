using SteelLotus.Core;
using SteelLotus.Core.Events;
using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackingEventsInvoke : MonoBehaviour
{
    [SerializeField]
    private Vector2 eventTimer;

    [SerializeField]
    private List<HackingEvent> hackingEvents = new List<HackingEvent>();

    private HackingEvent currentEvent;
    private MinigameController minigameController;
    private ScenesManager sceneManager;

    int currentTime = 0;


    private void Awake()
    {
        if (minigameController == null)
            minigameController = MainGameController.Instance.GetPropertyByType<MinigameController>();

        sceneManager = MainGameController.Instance.GetPropertyByType<ScenesManager>();

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

        if(sceneManager.SceneIndex == 1)
        {
            sceneManager.StartAnimationText();
        }

        GameEvents.Instance.CallOnEventStarted(currentEvent);
    }

    public bool eventStarted()
    {
        return this.currentEvent != null;
    }

    private void EvenStopped(bool win)
    {
        if(!win)
        {
            MainGameController.Instance.DecreaseHealth();
        }
        else
        {
            SoundManager.Instance.PlayOneShoot(SoundManager.Instance.AlertSource, SoundManager.Instance.AlertCollection.clips[2], 0.5f);
        }

        currentEvent = null;
        MainGameController.Instance.GameStarted = true;
    }


}
