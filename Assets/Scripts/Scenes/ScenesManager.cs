using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private List<SceneDataHolder> sceneDataHolders = new List<SceneDataHolder>();

    [SerializeField]
    private SwitchScenes switchScenes;

    [SerializeField, NaughtyAttributes.Scene]
    private string mainMenuScene;

    private int sceneIndex = 0;

    private SceneDataHolder lastScene = null;

    private void Awake()
    {
        lastScene = sceneDataHolders[sceneIndex];
    }

    public void ReturnToMainMenu()
    {
        SoundManager.Instance.StopAudio(SoundManager.Instance.AlertSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.AmbientSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.MusicSource, false);
        switchScenes.StartMovement(() => SceneManager.LoadScene(mainMenuScene));
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
        sceneDataHolders[sceneIndex].EnableScene();

        lastScene.DisableScene();

        lastScene = sceneDataHolders[sceneIndex];

        yield return null;
    }
}
