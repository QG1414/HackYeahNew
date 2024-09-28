using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationInvoke : MonoBehaviour
{
    [SerializeField]
    private DialogueOrder order;

    [SerializeField]
    private Notification elementNotification;


    private NotificationController notificationController = null;

    [ContextMenu("start dialogoue")]
    public void StartDialoue()
    {

        if (notificationController == null)
            notificationController = MainGameController.Instance.GetPropertyByType<NotificationController>();

        notificationController.Dialogue(order, elementNotification);

        MainGameController.Instance.BlockUnlockInteractions(true);
    }

    public void StartMonolog()
    {
        if (notificationController == null)
            notificationController = MainGameController.Instance.GetPropertyByType<NotificationController>();

        notificationController.Dialogue(order);

        MainGameController.Instance.BlockUnlockInteractions(true);
    }





}
