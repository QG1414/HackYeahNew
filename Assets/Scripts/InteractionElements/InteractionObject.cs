using SteelLotus.Core;
using SteelLotus.Core.Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField]
    private RectTransform objectTransform;
    [SerializeField]
    private Image image;

    [SerializeField]
    private UnityEvent onClick;

    [SerializeField]
    private UnityEvent onDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(onDrop != null)
                onDrop.Invoke();

            PlayerEvents.Instance.CallOnDrop();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = objectTransform.anchoredPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(onClick != null)
        {
            onClick.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = image.color * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = image.color / 1.2f;
    }
}
