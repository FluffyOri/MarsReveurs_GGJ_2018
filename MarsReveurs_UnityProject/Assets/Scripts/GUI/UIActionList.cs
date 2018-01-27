using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionList : MonoBehaviour
{
    public List<UIAvailableAction> AvailableActions;

    public UIAvailableAction GetAction(RoverInstruction instruction)
    {
        if (instruction != null)
        {
            for (int i = 0; i < this.AvailableActions.Count; i++)
            {
                if (this.AvailableActions[i].ActionName == instruction.GetType().Name.Replace("RoverInstruction_", string.Empty))
                {
                    return this.AvailableActions[i];
                }
            }
        }

        return null;
    }
}
