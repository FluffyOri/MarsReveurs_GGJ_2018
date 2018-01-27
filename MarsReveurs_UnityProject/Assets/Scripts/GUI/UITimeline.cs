using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeline : MonoBehaviour
{
    public int ActionCount = 10;
    public int Spacing = 2;
    public UIQueuedAction QueuedActionItemPrefab;

    public List<UIQueuedAction> QueuedActions = new List<UIQueuedAction>();
    private int lastTick = -1;

    public bool Dirty
    {
        get;
        set;
    }

    public void Start()
    {
        this.BuildActionList();
    }

    public void BuildActionList ()
    {
        this.QueuedActions.Clear();
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < this.ActionCount; i++)
        {
            UIQueuedAction newAction = (UIQueuedAction)Instantiate(this.QueuedActionItemPrefab, this.transform);
            RectTransform rectTransform = newAction.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.anchoredPosition = new Vector2(0, (rectTransform.sizeDelta.y + this.Spacing) * i * -1);
            this.QueuedActions.Add(newAction);
        } 
	}

    public void Update()
    {
        int currentTick = RoverController.CurrentTick;
        if (this.lastTick != currentTick)
        {
            this.lastTick = currentTick;
            this.Dirty = true;
        }

        if (this.Dirty)
        {
            int i = 0;
            int fromTick = RoverController.CurrentTick;
            int toTick = RoverController.CurrentTick + this.ActionCount;
            Debug.Log(string.Format("Enumerate instruction from {0} to {1}", fromTick, toTick));
            foreach (RoverInstruction instruction in RoverController.Instance.EnumerateInstructions(fromTick, toTick))
            {
                if (instruction != null)
                {
                    Debug.Log(instruction.GetType().Name);
                }

                this.QueuedActions[i].Refresh(instruction);
                i++;
            }

            this.Dirty = false;
        }
    }
}
