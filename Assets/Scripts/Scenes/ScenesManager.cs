using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private List<SceneDataHolder> sceneDataHolders = new List<SceneDataHolder>();

    [SerializeField]
    private SwitchScenes switchScenes;

    private int sceneIndex = 0;

    private SceneDataHolder lastScene = null;

    public void LoadNextScene()
    {
        sceneIndex += 1;

        if (sceneIndex >= sceneDataHolders.Count)
            return;

        StartCoroutine(Loading());
        
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(switchScenes.StartMovement());

        lastScene.DisableScene();

        lastScene = sceneDataHolders[sceneIndex];

        backgroundImage.sprite = lastScene.EnableScene();

        yield return new WaitForSeconds(switchScenes.StopMovement());
    }
}
