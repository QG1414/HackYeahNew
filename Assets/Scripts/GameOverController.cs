using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        GameEvents.Instance.OnGameOver += GameOverScreen;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOverScreen;
    }

    private void GameOverScreen()
    {
        MainGameController.Instance.GameStarted = false;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

 
}
