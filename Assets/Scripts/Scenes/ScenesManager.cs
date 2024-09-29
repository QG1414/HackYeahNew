using SteelLotus.Core;
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

    [SerializeField, NaughtyAttributes.Scene]
    private string gameScene;

    [SerializeField]
    private AnimatedText animatedText;

    private int sceneIndex = 0;

    private SceneDataHolder lastScene = null;
    private HackingEventsInvoke hackingEvents;

    public int SceneIndex { get => sceneIndex; }

    private void Awake()
    {
        lastScene = sceneDataHolders[sceneIndex];
        hackingEvents = MainGameController.Instance.GetPropertyByType<HackingEventsInvoke>();
    }

    public void ReturnToMainMenu()
    {
        SoundManager.Instance.StopAudio(SoundManager.Instance.AlertSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.AmbientSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.MusicSource, false);
        switchScenes.StartMovement(() => SceneManager.LoadScene(mainMenuScene));
    }

    public void ReturnToMainMenuInstant()
    {
        SoundManager.Instance.StopAudio(SoundManager.Instance.AlertSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.AmbientSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.MusicSource, false);
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Restart()
    {
        SoundManager.Instance.StopAudio(SoundManager.Instance.AlertSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.AmbientSource, false);
        SoundManager.Instance.StopAudio(SoundManager.Instance.MusicSource, false);
        SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource, SoundManager.Instance.MusicCollection.clips[1], true);
        SceneManager.LoadScene(gameScene);
    }

    public void LoadNextScene()
    {
        sceneIndex += 1;

        if (sceneIndex >= sceneDataHolders.Count)
        {
            sceneIndex = 0;
        }
        else
        {
            if (hackingEvents.eventStarted())
            {
                StartAnimationText();
            }
        }

        StartCoroutine(Loading());
        
    }

    public void StartAnimationText()
    {
        animatedText.StartAnimation();
    }

    private IEnumerator Loading()
    {
        sceneDataHolders[sceneIndex].EnableScene();

        lastScene.DisableScene();

        lastScene = sceneDataHolders[sceneIndex];

        yield return null;
    }
}
