using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Managers/Notification/NotificationData")]
public class NotificationData : ScriptableObject
{
    [SerializeField]
    private TalkingOptions talkingOption;

    [SerializeField]
    private List<string> dialogue = new List<string>();


    public TalkingOptions TalkingOptions { get => talkingOption; }
    public List<string> Dialogue { get => dialogue; }
}

public enum TalkingOptions
{
    Main,
    Other
}
