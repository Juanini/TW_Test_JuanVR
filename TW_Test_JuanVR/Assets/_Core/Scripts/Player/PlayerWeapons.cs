using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [BoxGroup("Components")]
    public GameObject shootingPos;
    [BoxGroup("Components")]
    public Pool bulletsPool;

    [BoxGroup("Properties")]
    public float shootTime;

    private bool canShoot = true;

    private WaitForSeconds timeToShoot;


    private void Start() 
    {
        bulletsPool.InitPool();    
        timeToShoot = new WaitForSeconds(shootTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shoot();    
        }        
    }

    public void Shoot()
    {
        if(!canShoot) { return; }

        canShoot = false;

        GameObject bulletObj = bulletsPool.GetPooledObject();
        bulletObj.transform.position = shootingPos.transform.position;
        bulletObj.gameObject.SetActive(true);

        StartCoroutine(EnableShooting());
    }

    private IEnumerator EnableShooting()
    {
        yield return timeToShoot;
        canShoot = true;
    }
}
