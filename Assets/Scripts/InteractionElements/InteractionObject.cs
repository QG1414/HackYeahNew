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
    protected RectTransform objectTransform;
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected ToolType toolType;

    [SerializeField]
    protected UnityEvent onClick;

    [SerializeField]
    protected UnityEvent onDrop;

    [SerializeField]
    protected UnityEvent onWrongToolDrop;

    public virtual void OnDrop(PointerEventData eventData)
    {

        if(eventData.pointerDrag != null)
        {
            ToolInteractions tool = eventData.pointerDrag.GetComponent<ToolInteractions>();

            if(tool != null && tool.ToolType == toolType)
            {
                if (onDrop != null)
                    onDrop.Invoke();

                PlayerEvents.Instance.CallOnDrop();
            }
            else if(tool != null)
            {
                if (onWrongToolDrop != null)
                    onWrongToolDrop.Invoke();
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(onClick != null)
        {
            onClick.Invoke();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        image.color = image.color * 1.2f;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        image.color = image.color / 1.2f;
    }
}
