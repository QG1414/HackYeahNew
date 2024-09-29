using SteelLotus.Core.Events;
using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HackingEvent : MonoBehaviour
{
    [SerializeField]
    private float maxTime;

    [SerializeField]
    private CanvasGroup hackingCanvas;

    [SerializeField]
    private CanvasGroup minigameCanvas;

    [SerializeField]
    private TMP_Text timerQuestion;

    [SerializeField]
    private TMP_Text timerMinigame;

    float currentTime = 0;

    public void StartMainScreen()
    {
        currentTime = (maxTime / 4f) * 1000;
        minigameCanvas.alpha = 1.0f;
        minigameCanvas.blocksRaycasts = true;
        minigameCanvas.interactable = true;
        CancelInvoke(nameof(UpdateMethodQuestion));
        InvokeRepeating(nameof(UpdateMethodMinigame), 0, 1f/1000f);
    }

    public virtual void Awake()
    {
        DisableScreen();
    }

    private void DisableScreen()
    {
        hackingCanvas.alpha = 0f;
        hackingCanvas.blocksRaycasts = false;
        hackingCanvas.interactable = false;
        minigameCanvas.alpha = 0f;
        minigameCanvas.blocksRaycasts = false;
        minigameCanvas.interactable = false;
    }

    public virtual void StartEvent()
    {
        currentTime = maxTime;
        timerQuestion.text = currentTime.ToString();
        hackingCanvas.alpha = 1.0f;
        hackingCanvas.blocksRaycasts = true;
        hackingCanvas.interactable = true;
        
        InvokeRepeating(nameof(UpdateMethodQuestion), 0, 1);
    }

    protected virtual void UpdateMethodMinigame()
    {
        currentTime -= 1;

        timerMinigame.text = currentTime.ToString();

        if (currentTime <= 0)
        {
            CancelInvoke(nameof(UpdateMethodMinigame));
            GameEvents.Instance.CallOnEventStopped(false);
            DisableScreen();
        }
    }

    protected virtual void UpdateMethodQuestion()
    {
        currentTime -= 1;

        timerQuestion.text = currentTime.ToString();

        if (currentTime <= 0)
        {
            CancelInvoke(nameof(UpdateMethodQuestion));
            GameEvents.Instance.CallOnEventStopped(false);
            DisableScreen();
        }
    }

    public virtual void FinishHacking(bool value)
    {
        CancelInvoke(nameof(UpdateMethodMinigame));
        GameEvents.Instance.CallOnEventStopped(value);
        DisableScreen();
    }
}
