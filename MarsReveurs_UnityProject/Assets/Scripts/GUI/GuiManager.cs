using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager : Singleton<GuiManager>
{
    public Canvas Canvas;
    public UITimeline Timeline;
    public UIActionList AvailableActionsPanel;
    public Text ScoreText;

    protected GuiManager()
    {

    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        this.ScoreText.text = string.Format("{0} Mars Points", RoverController.CurrentTick);
    }
}