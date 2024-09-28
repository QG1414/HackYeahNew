using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneDataHolder
{
    [SerializeField]
    private CanvasGroup objectsToEnable;


    public void EnableScene()
    {
        objectsToEnable.alpha = 1f;
        objectsToEnable.interactable = true;
        objectsToEnable.blocksRaycasts = true;
    }

    public void DisableScene()
    {
        objectsToEnable.alpha = 0f;
        objectsToEnable.interactable = false;
        objectsToEnable.blocksRaycasts = false;
    }
}
