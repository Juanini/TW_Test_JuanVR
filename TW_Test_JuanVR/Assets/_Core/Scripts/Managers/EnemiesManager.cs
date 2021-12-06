using GameEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public EnemiesContainer enemiesContainer;

    public EnemyData enemyDataGreen;
    public EnemyData enemyDataBlue;
    public EnemyData enemyDataRed;

    void Awake() 
    {
        SetupEvents();    
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

    // * =====================================================================================================================================
    // * SHOOT

    public void EnemyShoot()
    {

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
