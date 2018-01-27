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

            int index = GuiManager.Instance.Timeline.QueuedActions.FindIndex(match => match == this);
            if (index == 0)
            {
                this.ActionLabel.text = "Processing...";
            }
            else if (index <= GuiManager.Instance.Timeline.LockCount)
            {
                this.ActionLabel.text = "Locked!";
            }
            else
            {
                this.ActionLabel.text = "Waiting...";
            }
        }
        else
        {
            this.ActionIcon.sprite = null;
            this.ActionIcon.color = Color.clear;
            this.BackgroundImage.color = Color.white;
            this.normalColor = this.BackgroundImage.color;
            this.ActiveActionName = string.Empty;
            this.ActionLabel.text = string.Empty;
        }
    }

    public void OnDrop(PointerEventData data)
    {
        this.BackgroundImage.color = normalColor;

        UIAvailableAction action = this.GetDropUIAvailableAction(data);
        if (action != null)
        {
            int index = GuiManager.Instance.Timeline.QueuedActions.FindIndex(match => match == this);
            RoverController.Instance.PushInstruction(System.Type.GetType("RoverInstruction_" + action.ActionName), RoverController.CurrentTick + index);
            GuiManager.Instance.Timeline.Dirty = true;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        UIAvailableAction action = this.GetDropUIAvailableAction(data);
        if (action != null && string.IsNullOrEmpty(this.ActiveActionName))
        {
            this.BackgroundImage.color = this.HighlightColor;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        this.BackgroundImage.color = this.normalColor;
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
