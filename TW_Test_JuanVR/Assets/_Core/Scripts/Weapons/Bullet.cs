using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [BoxGroup("Properties")]
    public float speed;
    [BoxGroup("Properties")]
    public float disableTime;
    [BoxGroup("Properties")]
    public string colliderTag;

    private WaitForSeconds disableWait;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
        disableWait = new WaitForSeconds(disableTime);
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
        yield return disableWait;
        DisableBullet();
    }

    private void OnTriggerEnter2D(Collider2D _other) 
    {
        if(_other.tag != colliderTag) { return; }
        DisableBullet();
    }

    private void OnBecameInvisible() 
    {
        DisableBullet();
    }
}
