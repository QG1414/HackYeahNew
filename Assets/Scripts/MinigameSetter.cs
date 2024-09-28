using SteelLotus.Core.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSetter : MonoBehaviour
{
    [SerializeField]
    private Button buttonToConnect;

    private void Awake()
    {
        GameEvents.Instance.OnEventStarted += ConnectButtonToEvent;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnEventStarted -= ConnectButtonToEvent;
    }

    private void ConnectButtonToEvent(HackingEvent obj)
    {
        buttonToConnect.onClick.RemoveAllListeners();
        buttonToConnect.onClick.AddListener(() => obj.StartMainScreen());
    }
}
