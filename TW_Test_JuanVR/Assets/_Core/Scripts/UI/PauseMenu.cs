using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MenuBase
{
    public Button continueButton;
    public Button mainMenuButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    public override void InitScreen()
    {
        base.InitScreen();
        Time.timeScale = 0;
    }

    private void OnContinueClick()
    {
        Time.timeScale = 1;
        onExitScreen();
    }

    private void OnMainMenuClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConstants.S_GAME);
    }
}
