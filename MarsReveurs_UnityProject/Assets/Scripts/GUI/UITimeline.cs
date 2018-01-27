using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeline : MonoBehaviour
{
    public int ActionCount = 10;
    public int Spacing = 2;
    public UIQueuedAction QueuedActionItemPrefab;

    public List<UIQueuedAction> queuedActions = new List<UIQueuedAction>();

    public void Start()
    {
        this.BuildActionList();
    }

    public void BuildActionList ()
    {
        for (int i = 0; i < this.ActionCount; i++)
        {
            UIQueuedAction newAction = (UIQueuedAction)Instantiate(this.QueuedActionItemPrefab, this.transform);
            RectTransform rectTransform = newAction.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.anchoredPosition = new Vector2(0, (rectTransform.sizeDelta.y + this.Spacing) * i * -1);
            this.queuedActions.Add(newAction);
        } 
	}
}
