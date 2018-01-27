using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIAvailableAction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static string ActionIconsPathPrefix = "ActionIcons/action";

    public string ActionName;
    public bool DragOnSurfaces = true;
    public GameObject DraggedItemPrefab;
    public Image BackgroundImage;
    public Image ActionIcon;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    public void Start()
    {
        this.ActionIcon.sprite = Resources.Load<Sprite>(ActionIconsPathPrefix + this.ActionName);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = GuiManager.Instance.Canvas;
        if (canvas == null)
        {
            return;
        }

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        this.m_DraggingIcon = (GameObject)Instantiate(this.DraggedItemPrefab);

        this.m_DraggingIcon.transform.SetParent(canvas.transform, false);
        this.m_DraggingIcon.transform.SetAsLastSibling();

        UIDraggedItem draggedItem = this.m_DraggingIcon.GetComponent<UIDraggedItem>();

        draggedItem.Icon.sprite = this.ActionIcon.sprite;
        draggedItem.Background.color = this.BackgroundImage.color;

        if (this.DragOnSurfaces)
        {
            this.m_DraggingPlane = this.transform as RectTransform;
        }
        else
        {
            this.m_DraggingPlane = canvas.transform as RectTransform;
        }

        this.SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.m_DraggingIcon != null)
        {
            this.SetDraggedPosition(eventData);
        }
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        if (this.DragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        {
            this.m_DraggingPlane = eventData.pointerEnter.transform as RectTransform;
        }

        var rt = this.m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_DraggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = this.m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.m_DraggingIcon != null)
        {
            GameObject.Destroy(this.m_DraggingIcon);

        }

        this.m_DraggingIcon = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount > 0 && eventData.clickCount % 2 > 0)
        {
            string actionName = "RoverInstruction_" + this.ActionName;
            System.Type type = System.Type.GetType(actionName);
            RoverController.Instance.PushInstruction(type);
            GuiManager.Instance.Timeline.Dirty = true;
        }
    }
}
