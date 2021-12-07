using DG.Tweening;
using GameEventSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Ins;

    [HideInInspector]
    public LevelData levelActive;

    [BoxGroup("Levels")]
    public LevelData levelData_Layout1;
    public LevelData levelData_Layout2;


    [BoxGroup("Elements")]
    public Player player;
    [BoxGroup("Elements")]
    public GameObject enemyContainer;
    [BoxGroup("Elements")]
    public GameObject defaultBackgrond;

    [BoxGroup("SCORE")]
    public List<PointsUI> pointsUIPool;
    [BoxGroup("SCORE")]
    public Transform multiplier3Pos;
    [BoxGroup("SCORE")]
    public Transform multiplier2Pos;

    [BoxGroup("POWER UP")]
    public PowerUp powerUp;
    [BoxGroup("POWER UP")]
    public List<Transform> powerUpSpawnPos;

    [HideInInspector]
    public bool blockPlayerMovement = false;

    private int score;
    public int Score
    {
        get { return score; } 
        set { score = value; } 
    }

    private bool isGameOver = false;
    public bool IsGameOver
    {
        get { return isGameOver; } 
        set { isGameOver = value; } 
    }

    public static bool retryEneabled = false;
    public static int layoutSelected = 0;

    void Start()
    {
        Ins = this;    
        SetupEvents();

        if (retryEneabled)
        {
            retryEneabled = false;
            FadePanel.Ins.SetBack();

            Hashtable ht = new Hashtable();
            ht.Add(GameEventParam.LAYOUT_SELECTED, layoutSelected);
            GameEventManager.TriggerEvent(GameEvents.ON_LAYOUT_SELECT, ht);
        }
        else
        {
            MenuManager.Ins.ShowMenu(GameConstants.MENU_MAIN);
        }
    }

    // * =====================================================================================================================================
    // * LEVELS

    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void OnLevelComplete(Hashtable _ht)
    {
        MenuManager.Ins.ShowMenu(GameConstants.MENU_COMPLETE);
    }

    public void LayoutSelected(Hashtable _ht)
    {
        player.gameObject.SetActive(true);
        blockPlayerMovement = true;

        defaultBackgrond.gameObject.SetActive(false);

        layoutSelected = (int)_ht[GameEventParam.LAYOUT_SELECTED];

        switch (layoutSelected)
        {
            case GameConstants.LAYOUT_1:
            levelActive = levelData_Layout1;
            break;

            case GameConstants.LAYOUT_2:
            levelActive = levelData_Layout2;
            break;
        }

        GameObject backgroundObj = Instantiate(levelActive.background);
        backgroundObj.gameObject.SetActive(true);
        backgroundObj.transform.position = Vector3.zero;

        FadePanel.Ins.FadeOut();
    }

    private void OnLevelCounterEnd(Hashtable _ht)
    {
        blockPlayerMovement = false;   
        Invoke("SpawnPowerUp", Random.Range(3, 6));
    }

    public void OnGameOver(Hashtable _ht)
    {
        isGameOver = true;
        blockPlayerMovement = true;
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

    private int ScoreMultiplier()
    {
        Trace.Log("Multiplier " + enemyContainer.transform.position.y + " | " + multiplier3Pos.position.y);
        Trace.Log("Multiplier " +enemyContainer.transform.position.y + " | " + multiplier2Pos.position.y);

        if(enemyContainer.transform.position.y >= multiplier3Pos.position.y)
        {
            Trace.Log("Multiplier 3");
            return 3;
        }
        else if(enemyContainer.transform.position.y >= multiplier2Pos.position.y)
        {
            Trace.Log("Multiplier 2");
            return 2;
        }
        
        Trace.Log("Multiplier 1");
        return 1;
    }

    // * =====================================================================================================================================
    // * POWER UP

    private void SpawnPowerUp()
    {
        int randomPos = Random.Range(0, powerUpSpawnPos.Count);
        powerUp.transform.position = powerUpSpawnPos[randomPos].transform.position;
        powerUp.gameObject.SetActive(true);
    }

    public void SpwnPowerUpDelay()
    {
        Invoke("SpawnPowerUp", 3);
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
                pointsUIPool[i].AddPoints((int)_ht[GameEventParam.ENEMY_POINTS]  * ScoreMultiplier(), 
                                         (Vector3)_ht[GameEventParam.ENEMY_POS],
                                         ScoreMultiplier());
                break;
            }
        }
    }


     // * =====================================================================================================================================
    // * Events

    private void SetupEvents()
    {
        GameEventManager.StartListening(GameEvents.ON_LEVEL_COMPLETE, OnLevelComplete);
        GameEventManager.StartListening(GameEvents.ON_LEVEL_COUNTER_END, OnLevelCounterEnd);
        GameEventManager.StartListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        GameEventManager.StartListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StartListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }

    private void DestroyEvents()
    {
        GameEventManager.StopListening(GameEvents.ON_LEVEL_COMPLETE, OnLevelComplete);
        GameEventManager.StartListening(GameEvents.ON_LEVEL_COUNTER_END, OnLevelCounterEnd);
        GameEventManager.StopListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        GameEventManager.StopListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StopListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }
}
