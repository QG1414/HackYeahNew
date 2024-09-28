using SteelLotus.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    [SerializeField]
    List<MinigameData> minigameDatas = new List<MinigameData>();



    private IMinigame currentMinigame;

    public void StartMinigame(IMinigame minigame)
    {
        MainGameController.Instance.BlockUnlockInteractions(true);

        foreach (MinigameData data in minigameDatas)
        {
            if(data.MinigameType == minigame.MinigameType)
            {
                currentMinigame = minigame;
                data.Minigame.StartMinigame();
                data.MinigameCanvasGroup.alpha = 1;
                data.MinigameCanvasGroup.interactable = true;
                data.MinigameCanvasGroup.blocksRaycasts = true;
            }
        }
    }

    public void StopMinigame(bool value)
    {
        MainGameController.Instance.BlockUnlockInteractions(false);

        if (currentMinigame != null)
        {
            currentMinigame.ExitMinigameBool(value);
        }


        foreach (MinigameData data in minigameDatas)
        {
            if (data.MinigameType == currentMinigame.MinigameType)
            {
                data.MinigameCanvasGroup.alpha = 0;
                data.MinigameCanvasGroup.interactable = false;
                data.MinigameCanvasGroup.blocksRaycasts = false;
            }
        }

        currentMinigame = null;

    }

    public bool GeneratorMinigameActive()
    {
        return currentMinigame != null;
    }
}

[Serializable]
public class MinigameData
{
    [SerializeField]
    MinigameType type;

    [SerializeField]
    private CanvasGroup minigameScreen;

    [SerializeField]
    private Minigame minigame;

    public MinigameType MinigameType { get => type; }
    public CanvasGroup MinigameCanvasGroup { get => minigameScreen; }
    public Minigame Minigame { get => minigame; }
}
