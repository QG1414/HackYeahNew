using SteelLotus.Core;
using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField, NaughtyAttributes.Scene]
    private string gameScene;

    [SerializeField]
    private CanvasGroup interactionBlocker;

    [SerializeField]
    private CanvasGroup secondBlocker;

    [SerializeField]
    private Vector2 startPosition;

    [SerializeField]
    private Vector2 movementChange;

    [SerializeField]
    private float movementDuration;


    [SerializeField]
    private RectTransform settingsTransform;

    [SerializeField]
    private RectTransform creditsTransform;


    [SerializeField]
    private RectTransform changeScreen;

    private void Awake()
    {
        AdditionalFunctions.Instance.ObjectMovement(false, changeScreen, new Vector2(2000, 0), new Vector2(2000, 0), 0.6f, () => SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource, SoundManager.Instance.MusicCollection.clips[0], true));
    }

    public void StartGame()
    {
        SoundManager.Instance.StopAll();
        SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource, SoundManager.Instance.MusicCollection.clips[1], true);
        AdditionalFunctions.Instance.ObjectMovement(true, changeScreen, new Vector2(2000, 0), new Vector2(-2000, 0), 0.6f, () => SceneManager.LoadScene(gameScene));
    }

    public void Settings()
    {
        interactionBlocker.blocksRaycasts = true;
        secondBlocker.blocksRaycasts=true;
        AdditionalFunctions.Instance.ObjectMovement(true, settingsTransform, movementChange, startPosition, movementDuration, () => secondBlocker.blocksRaycasts = false);
    }

    public void CloseSettings()
    {
        AdditionalFunctions.Instance.ObjectMovement(false, settingsTransform, movementChange, startPosition, movementDuration, () => interactionBlocker.blocksRaycasts = false);
    }

    public void Credits()
    {
        secondBlocker.blocksRaycasts = true;
        interactionBlocker.blocksRaycasts = true;
        AdditionalFunctions.Instance.ObjectMovement(true, creditsTransform, movementChange, startPosition, movementDuration, () => secondBlocker.blocksRaycasts = false);
    }

    public void CloseCredits()
    {
        AdditionalFunctions.Instance.ObjectMovement(false, creditsTransform, movementChange, startPosition, movementDuration, () => interactionBlocker.blocksRaycasts = false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
