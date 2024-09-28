using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DialogueOrder
{
    [SerializeField]
    List<NotificationData> order = new List<NotificationData>();

    public List<NotificationData> GetOrder()
    {
        return order;
    }
}
