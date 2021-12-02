using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [BoxGroup("Properties")]
    public float speed;
    public float disableTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void OnEnable() 
    {
        Shoot();    
    }

    private void Shoot()
    {
        rb.velocity = new Vector2(0, speed);
        StartCoroutine(DisableDelay());
    }

    public void DisableBullet()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator DisableDelay()
    {
        yield return new WaitForSeconds(disableTime);
        DisableBullet();
    }

    private void OnTriggerEnter2D(Collider2D _other) 
    {
        if(_other.tag != GameConstants.TAG_ENEMY) { return; }

        DisableBullet();
    }

    private void OnBecameInvisible() 
    {
        DisableBullet();
    }
}
