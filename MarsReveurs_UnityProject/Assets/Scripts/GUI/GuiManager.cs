using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager : Singleton<GuiManager>
{
    public Canvas Canvas;
    public UITimeline Timeline;
    public UIActionList AvailableActionsPanel;

    protected GuiManager()
    {

    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}