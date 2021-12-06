using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MenuBase
{
    public Button retryButton;
    public Button mainMenuButton;

    void Start()
    {
        retryButton.onClick.AddListener(OnRetryClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnRetryClick()
    {
        
    }

    private void OnMainMenuClick()
    {
        SceneManager.LoadScene(GameConstants.S_GAME);
    }
}
