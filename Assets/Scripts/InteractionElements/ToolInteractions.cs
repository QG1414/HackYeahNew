using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolInteractions : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;


    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Image image;
    [SerializeField]
    private ToolType toolType;

    [SerializeField]
    private UnityEvent onClick;

    private Vector3 startPosition = Vector3.zero;

    private bool inReplacement = false;

    public ToolType ToolType { get => toolType; }

    private void Awake()
    {
        canvas = MainGameController.Instance.GetPropertyByName<Canvas>("canvas");

        if(rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (image == null)
            image = GetComponentInChildren<Image>();
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
        inReplacement = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startPosition;
        inReplacement = false;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = image.color * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = image.color / 1.2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(onClick != null)
            onClick.Invoke();
    }
}

public enum ToolType
{
    Potentiometer,
    Button,
    Pendrive
}
