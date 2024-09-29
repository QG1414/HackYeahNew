using SteelLotus.Core;
using SteelLotus.Sounds;
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

        SoundManager.Instance.PlayOneShoot(SoundManager.Instance.AlertSource, SoundManager.Instance.AlertCollection.clips[2], 0.5f);

        controller.StopMinigame(succeed);
    }
}
