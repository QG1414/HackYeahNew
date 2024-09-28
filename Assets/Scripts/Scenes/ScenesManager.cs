using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private List<SceneDataHolder> sceneDataHolders = new List<SceneDataHolder>();

    [SerializeField]
    private SwitchScenes switchScenes;

    private int sceneIndex = 0;

    private SceneDataHolder lastScene = null;

    private void Awake()
    {
        lastScene = sceneDataHolders[sceneIndex];
    }

    public void LoadNextScene()
    {
        sceneIndex += 1;

        if (sceneIndex >= sceneDataHolders.Count)
            sceneIndex = 0;

        StartCoroutine(Loading());
        
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(switchScenes.StartMovement());

        lastScene.DisableScene();

        lastScene = sceneDataHolders[sceneIndex];

        lastScene.EnableScene();

        yield return new WaitForSeconds(switchScenes.StopMovement());
    }
}
