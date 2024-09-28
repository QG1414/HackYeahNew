using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    protected bool succeed = false;
    protected bool minigameActive = false;

    public virtual void StartMinigame()
    {
        minigameActive = true;
    }

    public virtual void EndMinigame()
    {
        minigameActive = false;
        MinigameController controller = MainGameController.Instance.GetPropertyByType<MinigameController>();

        controller.StopMinigame(succeed);
    }
}
