using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Generator : InteractionObject, IMinigame
{
    [SerializeField]
    private Image generatorFillImage;

    [SerializeField]
    private float maxWorkTime;

    [SerializeField]
    private MinigameType minigameType;

    private float currentWorkTime = 0;

    public MinigameType MinigameType { get => minigameType; set => minigameType = value; }

    private MinigameController minigameController;

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
        if (value)
        {
            currentWorkTime = maxWorkTime;
            UpdateVisuals();
        }
    }

    private void Awake()
    {
        currentWorkTime = maxWorkTime;

        UpdateVisuals();

        GameEvents.Instance.OnSecondUpdate += UpdateMethod;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnSecondUpdate -= UpdateMethod;
    }


    private void UpdateMethod()
    {
        currentWorkTime -= 1f;

        UpdateVisuals();



        if (currentWorkTime <= 0)
        {
            MainGameController.Instance.DecreaseHealth();
            currentWorkTime = maxWorkTime;
        }
    }

    private void UpdateVisuals()
    {
        generatorFillImage.fillAmount = currentWorkTime / maxWorkTime;
        generatorFillImage.color = new Color(1, generatorFillImage.fillAmount, generatorFillImage.fillAmount, 1);

        GameEvents.Instance.CallOnGeneratorChange(currentWorkTime, maxWorkTime);
    }

}
