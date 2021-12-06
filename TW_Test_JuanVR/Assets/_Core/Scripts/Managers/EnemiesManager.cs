using Sirenix.OdinInspector;
using GameEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Ins;

    public EnemiesContainer enemiesContainer;

    [BoxGroup("Elements")]
    public Pool bulletPool;

    [BoxGroup("DATA")]
    public EnemyData enemyDataGreen;
    [BoxGroup("DATA")]
    public EnemyData enemyDataBlue;
    [BoxGroup("DATA")]
    public EnemyData enemyDataRed;

    private WaitForSeconds enemyShootWait;

    private List<Enemy> enemyList;

    void Awake() 
    {
        Ins = this;
        SetupEvents();
        SetEnemies();    
        bulletPool.InitPool();
    }

    void OnDestroy() 
    {
        DestroyEvents();    
    }

    public void LayoutSelected(Hashtable _ht)
    {
        enemiesContainer.gameObject.SetActive(true);
        SetEnemiesRandom();
    }

    private void OnLevelCounterEnd(Hashtable _ht)
    {
        InvokeRepeating("EnemyShoot", 0, LevelManager.Ins.levelActive.enemyShootTime);
    }

    // * =====================================================================================================================================
    // * 

    private void OnEnemyDead(Hashtable _ht)
    {
        enemyList.Remove((Enemy)_ht[GameEventParam.ENEMY_REF]);

        if (enemyList.Count <= 0)
        {
            Invoke("EndLevelDelay", 2f);
        }
    }

    private void EndLevelDelay()
    {
        GameEventManager.TriggerEvent(GameEvents.ON_LEVEL_COMPLETE);
    }

    // * =====================================================================================================================================
    // * SHOOT

    public void EnemyShoot()
    {
        if(LevelManager.Ins.IsGameOver) { return; }
        if(enemyList.Count <= 0) { return; }

        int randomPos;
        randomPos = Random.Range(0, enemyList.Count);
        enemyList[randomPos].Shoot();

    }

    // * =====================================================================================================================================
    // * 

    public void SetEnemiesRandom()
    {
        for (int i = 0; i < enemiesContainer.enemyRowList.Count; i++)
        {
            for (int j = 0; j < enemiesContainer.enemyRowList[i].enemyList.Count; j++)
            {
                int randomNum = Random.Range(1,101);

                Trace.Log(randomNum.ToString());

                if (randomNum <= 30)
                    enemiesContainer.enemyRowList[i].enemyList[j].SetupEnemy(enemyDataGreen);
                else if (randomNum <= 60)
                    enemiesContainer.enemyRowList[i].enemyList[j].SetupEnemy(enemyDataBlue);
                else
                    enemiesContainer.enemyRowList[i].enemyList[j].SetupEnemy(enemyDataRed);
            }
        }
    }

    public void SetEnemies()
    {
        enemyList = new List<Enemy>();

        for (int i = 0; i < enemiesContainer.enemyRowList.Count; i++)
        {
            for (int j = 0; j < enemiesContainer.enemyRowList[i].enemyList.Count; j++)
            {
                enemyList.Add(enemiesContainer.enemyRowList[i].enemyList[j]);
                enemiesContainer.enemyRowList[i].enemyList[j].posInList = enemyList.Count - 1;
            }
        }
    }

    // * =====================================================================================================================================
    // * Events

    private void SetupEvents()
    {
        GameEventManager.StartListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        GameEventManager.StartListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
        GameEventManager.StartListening(GameEvents.ON_LEVEL_COUNTER_END, OnLevelCounterEnd);
    }

    private void DestroyEvents()
    {
        GameEventManager.StopListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        GameEventManager.StopListening(GameEvents.ON_LEVEL_COUNTER_END, OnLevelCounterEnd);
        GameEventManager.StopListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }
}
