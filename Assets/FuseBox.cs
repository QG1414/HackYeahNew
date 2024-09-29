using SteelLotus.Core;
using SteelLotus.Core.Events;
using SteelLotus.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FuseBox : InteractionObject, IMinigame
{
    [SerializeField]
    private MinigameType minigameType;

    [SerializeField]
    private Vector2 possibleTimes;

    [SerializeField]
    private Image indicator;

    [SerializeField]
    private float effectTime;

    private bool startEffect = false;

    public MinigameType MinigameType { get => minigameType; set => minigameType = value; }

    int currentTime = 0;

    MinigameController minigameController;
    private Coroutine effectCoroutine;


    public override void OnDrop(PointerEventData eventData)
    {
        if (minigameController == null)
            minigameController = MainGameController.Instance.GetPropertyByType<MinigameController>();

        if (eventData.pointerDrag != null)
        {
            ToolInteractions tool = eventData.pointerDrag.GetComponent<ToolInteractions>();

            if (tool != null && tool.ToolType == toolType)
            {
                if (onDrop != null)
                    onDrop.Invoke();

                PlayerEvents.Instance.CallOnDrop();
                minigameController.StartMinigame(this);
            }
            else if (tool != null)
            {
                if (onWrongToolDrop != null)
                    onWrongToolDrop.Invoke();
            }
        }

    }

    public void ExitMinigameBool(bool value)
    {
        StopEffect();
        MainGameController.Instance.GeneratorMultiplaier = 1f;
        startEffect = false;
    }


    private void Awake()
    {
        GameEvents.Instance.OnSecondUpdate += MethodUpdate;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnSecondUpdate -= MethodUpdate;
    }

    private void MethodUpdate()
    {
        if (startEffect)
            return;

        int value = Mathf.FloorToInt(Random.Range(possibleTimes.x, possibleTimes.y));

        if(currentTime >= value && !MainGameController.Instance.GeneratorCritical)
        {
            startEffect = true;
            StartDestroyed();
            currentTime = 0;
        }

        currentTime += 1;
    }

    private void StartDestroyed()
    {
        StartEffect();
        MainGameController.Instance.GeneratorMultiplaier = 3f;
    }

    public void StartEffect()
    {
        indicator.color = new Color(1, 1, 1, 1);
        effectCoroutine = StartCoroutine(EffectActive());
        SoundManager.Instance.PlayClip(SoundManager.Instance.AlertSource, SoundManager.Instance.AlertCollection.clips[0], true);
    }

    public void StopEffect()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
        }

        indicator.color = new Color(1, 1, 1, 1);
        SoundManager.Instance.StopAudio(SoundManager.Instance.AlertSource);
    }

    private IEnumerator EffectActive()
    {
        float timer = 0;
        while (true)
        {
            timer = 0;
            Color startColor = indicator.color;
            while (indicator.color.r > 0.5f)
            {
                indicator.color = Color.Lerp(startColor, new Color(0.5f, 0.5f, 0.5f, 1), timer / effectTime);
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0;
            startColor = indicator.color;

            while (indicator.color.r < 1f)
            {
                indicator.color = Color.Lerp(startColor, new Color(1f, 1f, 1f, 1), timer / effectTime);
                timer += Time.deltaTime;
                yield return null;
            }

            yield return null;

        }
    }
}
