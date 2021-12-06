using GameEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public List<EnemyRow> enemyRowList;

    void Awake() 
    {
        GameEventManager.StartListening(GameEvents.ON_LEVEL_COUNTER_END, StartMovement);
    }

    void OnDestroy() 
    {
        GameEventManager.StopListening(GameEvents.ON_LEVEL_COUNTER_END, StartMovement);
    }

    private void StartMovement(Hashtable _ht)
    {
        InvokeRepeating("Move", 0, LevelManager.Ins.levelActive.enemyMoveDownTime);
    }

    public void Move()
    {
        if(LevelManager.Ins.IsGameOver) { return; }

        Vector2 v = transform.position;
        v.y -= 0.15f;
        
        transform.position = v;
    }
}
