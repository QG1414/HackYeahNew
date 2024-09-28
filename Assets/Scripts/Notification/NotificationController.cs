using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField]
    private Notification mainNotification;

    public void Dialogue(DialogueOrder order, Notification otherNotification)
    {
        StartCoroutine(MoveInDialogoue(order, otherNotification));
    }

    public void Dialogue(DialogueOrder order)
    {
        StartCoroutine(MoveInDialogoue(order));
    }


    private IEnumerator MoveInDialogoue(DialogueOrder order, Notification otherNotification)
    {
        int elementIndexMain = 0;
        int elementIndexOther = 0;

        List<NotificationData> notificationElements = order.GetOrder();

        foreach(NotificationData notification in notificationElements)
        {

            if (notification.TalkingOptions is TalkingOptions.Main)
            {

                elementIndexOther = 0;
                if (elementIndexMain == 0)
                {
                    otherNotification.StopDialogue();
                    mainNotification.StartDialogue();
                }

                foreach(string text in notification.Dialogue)
                {

                    mainNotification.ChangeText(text);

                    while (!Input.anyKeyDown)
                    {
                        yield return null;
                    }

                    bool skiped = mainNotification.SkipDialogue();

                    if(skiped)
                    {
                        yield return new WaitForSeconds(0.2f);

                        while (!Input.anyKeyDown)
                        {
                            yield return null;
                        }
                    }

                    yield return new WaitForSeconds(0.2f);
                }

                elementIndexMain += 1;
            }
            else
            {
                elementIndexMain = 0;

                if (elementIndexOther == 0)
                {
                    mainNotification.StopDialogue();
                    otherNotification.StartDialogue();
                }

                foreach (string text in notification.Dialogue)
                {
                    otherNotification.ChangeText(text);

                    while (!Input.anyKeyDown)
                    {
                        yield return null;
                    }

                    bool skiped = otherNotification.SkipDialogue();

                    if (skiped)
                    {
                        yield return new WaitForSeconds(0.2f);

                        while (!Input.anyKeyDown)
                        {
                            yield return null;
                        }
                    }

                    yield return new WaitForSeconds(0.2f);
                }

                elementIndexOther += 1;
            }
        }

        otherNotification.StopDialogue();
        mainNotification.StopDialogue();
    }


    private IEnumerator MoveInDialogoue(DialogueOrder order)
    {
        int elementIndexMain = 0;

        List<NotificationData> notificationElements = order.GetOrder();

        foreach (NotificationData notification in notificationElements)
        {
            if (elementIndexMain == 0)
            {
                mainNotification.StartDialogue();
            }

            foreach (string text in notification.Dialogue)
            {
                mainNotification.ChangeText(text);

                while (!Input.anyKeyDown)
                {
                    yield return null;
                }

                bool skiped = mainNotification.SkipDialogue();

                if (skiped)
                {
                    while (!Input.anyKeyDown)
                    {
                        yield return null;
                    }
                }
            }

            elementIndexMain += 1;
        }

        mainNotification.StopDialogue();
    }

}
