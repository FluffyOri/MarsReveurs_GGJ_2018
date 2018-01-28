using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQueuedAction : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image BackgroundImage;
    public Image ActionIcon;
    public Text ActionLabel;
    public Color HighlightColor = Color.yellow;
    public Color LockColor = Color.red;
    public Image LockIcon;
    public Image ProcessIcon;

    private bool normalHighlight;
    private bool lockHighlight;

    public string ActiveActionName
    {
        get;
        set;
    }

    private Color normalColor;

    public void OnEnable()
    {
        if (this.BackgroundImage != null)
        {
            this.normalColor = this.BackgroundImage.color;
        }
    }

    public void Refresh(RoverInstruction instruction)
    {
        UIAvailableAction action = GuiManager.Instance.AvailableActionsPanel.GetAction(instruction);
        this.Refresh(action);
    }

    public void Refresh(UIAvailableAction action)
    {
        if (action != null)
        {
            this.ActionIcon.overrideSprite = action.ActionIcon.sprite;
            this.ActionIcon.color = Color.white;
            this.BackgroundImage.color = action.BackgroundImage.color;
            this.normalColor = this.BackgroundImage.color;
            this.ActiveActionName = action.ActionName;
            this.ActionLabel.text = action.LocalizedTitle;

            int index = GuiManager.Instance.Timeline.QueuedActions.FindIndex(match => match == this);
            if (index == 0)
            {
                this.LockIcon.color = Color.clear;
                this.ProcessIcon.color = Color.white;
            }
            else if (index <= GuiManager.Instance.Timeline.LockCount)
            {
                this.LockIcon.color = Color.white;
                this.ProcessIcon.color = Color.clear;
            }
            else
            {
                this.LockIcon.color = Color.clear;
                this.ProcessIcon.color = Color.clear;
            }
        }
        else
        {
            this.ActionIcon.sprite = null;
            this.ActionIcon.color = Color.clear;
            this.normalColor = Color.white;
            this.BackgroundImage.color = this.normalHighlight ? this.HighlightColor : this.normalColor;
            this.ActiveActionName = string.Empty;
            this.ActionLabel.text = string.Empty;
            this.LockIcon.color = this.lockHighlight ? this.LockColor : Color.clear;
            this.ProcessIcon.color = Color.clear;
        }
    }

    public void OnDrop(PointerEventData data)
    {
        this.BackgroundImage.color = normalColor;
        this.LockIcon.color = Color.clear;
        this.normalHighlight = false;
        this.lockHighlight = false;

        UIAvailableAction action = this.GetDropUIAvailableAction(data);
        int index = GuiManager.Instance.Timeline.QueuedActions.FindIndex(match => match == this);
        if (action != null && index > GuiManager.Instance.Timeline.LockCount)
        {
            RoverController.Instance.PushInstruction(System.Type.GetType("RoverInstruction_" + action.ActionName), RoverController.CurrentTick + index);
            GuiManager.Instance.Timeline.Dirty = true;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        UIAvailableAction action = this.GetDropUIAvailableAction(data);
        int index = GuiManager.Instance.Timeline.QueuedActions.FindIndex(match => match == this);
        if (action != null && string.IsNullOrEmpty(this.ActiveActionName))
        {
            if (index > GuiManager.Instance.Timeline.LockCount)
            {
                this.BackgroundImage.color = this.HighlightColor;
                this.normalHighlight = true;
            }
            else
            {
                this.LockIcon.color = this.LockColor;
                this.lockHighlight = true;
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        this.BackgroundImage.color = this.normalColor;
        this.LockIcon.color = Color.clear;
        this.normalHighlight = false;
        this.lockHighlight = false;
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
