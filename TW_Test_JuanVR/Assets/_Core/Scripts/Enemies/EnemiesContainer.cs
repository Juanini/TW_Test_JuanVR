using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public List<EnemyRow> enemyRowList;

    void Start() 
    {
        InvokeRepeating("Move", 5, 0.5f);
    }

    public void Move()
    {
        Vector2 v = transform.position;
        v.y -= 0.15f;
        
        transform.position = v;
    }
}
