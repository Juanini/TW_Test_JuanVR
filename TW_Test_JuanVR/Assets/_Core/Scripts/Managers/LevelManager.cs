using DG.Tweening;
using GameEventSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Ins;

    [BoxGroup("Elements")]
    public Player player;
    [BoxGroup("Elements")]
    public GameObject enemyContainer;

    [BoxGroup("SCORE")]
    public List<PointsUI> pointsUIPool;

    private int score;
    private bool isGameOver = false;

    void Start()
    {
        Ins = this;    
        SetupEvents();

        MenuManager.Ins.ShowMenu(GameConstants.MENU_MAIN);
    }

    // * =====================================================================================================================================
    // * LEVELS

    public void LayoutSelected(Hashtable _ht)
    {
        player.gameObject.SetActive(true);
        FadePanel.Ins.FadeOut();
    }

    public void OnGameOver(Hashtable _ht)
    {
        isGameOver = true;
    }

    // * =====================================================================================================================================
    // * SCORE

    public void AddScore(int _score)
    {
        int oldScore = score;
        score += _score;

        string format = "00000";

        DOTween.To(()=> oldScore, x=> oldScore = x, score, 0.8f)
        .OnUpdate(()=> UIManager.Ins.scoreText.text = oldScore.ToString(format))
        .SetEase(Ease.Linear);
    }

    // * =====================================================================================================================================
    // * ENEMIES

    public void OnEnemyDead(Hashtable _ht)
    {
        for (int i = 0; i < pointsUIPool.Count; i++)
        {
            if (!pointsUIPool[i].gameObject.activeInHierarchy)
            {
                pointsUIPool[i].gameObject.SetActive(true);
                pointsUIPool[i].AddPoints((int)_ht[GameEventParam.ENEMY_POINTS], 
                                         (Vector3)_ht[GameEventParam.ENEMY_POS]);
                break;
            }
        }
    }


     // * =====================================================================================================================================
    // * Events

    private void SetupEvents()
    {
        GameEventManager.StartListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        GameEventManager.StartListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StartListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }

    private void DestroyEvents()
    {
        GameEventManager.StopListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        GameEventManager.StopListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StopListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }
}
