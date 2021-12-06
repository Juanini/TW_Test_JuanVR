using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelCompleteMenu : MenuBase
{
    public TextMeshProUGUI scoreText;
    public Button mainMenuButton;
    
    private float scoreAnimTime = 1.5f;
    private float scoreTemp = 0;

    private Tween scoreMovementTween;
    private Vector3 punchPosVector = new Vector3(0, 10, 0);

    void Start()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    public override void InitScreen()
    {
        base.InitScreen();

        mainMenuButton.interactable = false;

        DOTween .To(()=> scoreTemp, x=> scoreTemp = x, LevelManager.Ins.Score, scoreAnimTime)
                .OnUpdate(OnScoreUpdate)
                .OnComplete(OnScoreAnimComplete)
                .SetDelay(1);
    }

    private void OnScoreUpdate()
    {
        scoreText.text = scoreTemp.ToString("00000");

        if(scoreMovementTween == null)
        {
            scoreMovementTween = scoreText.transform.DOShakePosition(0.15f, punchPosVector).SetEase(Ease.InOutSine);
        }
        else if(!scoreMovementTween.IsPlaying())
        {
            scoreMovementTween = scoreText.transform.DOShakePosition(0.15f, punchPosVector).SetEase(Ease.InOutSine);
        }
    }

    private void OnScoreAnimComplete()
    {
        mainMenuButton.interactable = true;
    }

    private void OnMainMenuClick()
    {
        SceneManager.LoadScene(GameConstants.S_GAME);
    }
}
