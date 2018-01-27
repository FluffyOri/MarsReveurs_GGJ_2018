using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQueuedAction : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image Background;
    public Image ActionIcon;
    public Text ActionLabel;
    public Color HighlightColor = Color.yellow;

    public string ActiveActionName
    {
        get;
        set;
    }

    private Color normalColor;

    public void OnEnable()
    {
        if (this.Background != null)
        {
            this.normalColor = this.Background.color;
        }
    }

    public void OnDrop(PointerEventData data)
    {
        this.Background.color = normalColor;

        UIAvailableAction action = this.GetDropUIAvailableAction(data);
        if (action != null)
        {
            this.ActionIcon.overrideSprite = action.ActionIcon.sprite;
            this.ActionIcon.color = action.ActionIcon.color;
            this.ActiveActionName = action.ActionName;
            this.ActionLabel.text = this.ActiveActionName;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        UIAvailableAction action = this.GetDropUIAvailableAction(data);
        if (action != null)
        {
            this.Background.color = this.HighlightColor;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        this.Background.color = this.normalColor;
    }

    private UIAvailableAction GetDropUIAvailableAction(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        UIAvailableAction action = originalObj.GetComponent<UIAvailableAction>();
                
        return action;
    }
}
