using SteelLotus.Core.Events;
using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteelLotus.Sounds;

public class FinalGameScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        GameEvents.Instance.OnGameEnd += EndScreen;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameEnd -= EndScreen;
    }

    private void EndScreen()
    {
        SoundManager.Instance.StopAll(true);

        MainGameController.Instance.GameStarted = false;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
