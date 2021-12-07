using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigidbody2D;

    private bool diablePower = false;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable() 
    {
        if(diablePower) { return; }
        rigidbody2D.velocity = new Vector2(0, -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D _other) 
    {
        if(_other.tag == GameConstants.TAG_PLAYER)
        {
            diablePower = true;
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible() 
    {
        LevelManager.Ins.SpwnPowerUpDelay();
    }
}
