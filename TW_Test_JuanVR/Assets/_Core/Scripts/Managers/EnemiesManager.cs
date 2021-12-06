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

    void Awake() 
    {
        Ins = this;
        SetupEvents();    
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

        InvokeRepeating("EnemyShoot", 5, 2);
    }

    // * =====================================================================================================================================
    // * SHOOT

    public void EnemyShoot()
    {
        if(LevelManager.Ins.IsGameOver) { return; }

        int randomColum, randomRow;
        randomColum = Random.Range(0, enemiesContainer.enemyRowList.Count);
        randomRow = Random.Range(0, enemiesContainer.enemyRowList[randomColum].enemyList.Count);

        enemiesContainer.enemyRowList[randomColum].enemyList[randomRow].Shoot();
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

    // * =====================================================================================================================================
    // * Events

    private void SetupEvents()
    {
        // GameEventManager.StartListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        // GameEventManager.StartListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StartListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }

    private void DestroyEvents()
    {
        // GameEventManager.StopListening(GameEvents.ON_ENEMY_DEAD, OnEnemyDead);
        // GameEventManager.StopListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StopListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }
}
