using SteelLotus.Core;
using SteelLotus.Core.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private DialogueOrder order;

    [SerializeField]
    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();

    [SerializeField]
    private RectTransform indicator;

    private NotificationController notificationController;

    private void Awake()
    {
        GameEvents.Instance.OnDialogueChange += MovePointer;
        GameEvents.Instance.OnDialogueEnd += EndTutorial;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnDialogueChange -= MovePointer;
        GameEvents.Instance.OnDialogueEnd -= EndTutorial;
    }

    public void StartTutorial()
    {
        MainGameController.Instance.BlockUnlockInteractions(true);
        if (notificationController == null)
            notificationController = MainGameController.Instance.GetPropertyByType<NotificationController>();

        notificationController.Dialogue(order);
    }

    public void EndTutorial(DialogueOrder order)
    {
        if (this.order != order)
            return;

        indicator.gameObject.SetActive(false);
        MainGameController.Instance.BlockUnlockInteractions(false);
        MainGameController.Instance.GameStarted = true;
    }

    private void MovePointer(DialogueOrder order, int indexMain, int indexSecond)
    {
        if (this.order != order)
            return;

        bool elementFound = false;

        foreach (TutorialStep step in tutorialSteps)
        {
            if (step.dialogoueID == indexSecond)
            {
                indicator.anchoredPosition = step.indicatorPosition;
                indicator.gameObject.SetActive(true);
                elementFound = true;
                break;
            }
        }

        if(!elementFound)
            indicator.gameObject.SetActive(false);
    }


}

[Serializable]
public class TutorialStep
{
    public int dialogoueID;
    public Vector2 indicatorPosition;
}
