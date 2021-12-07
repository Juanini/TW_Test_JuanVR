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
        if (LevelManager.Ins.blockPlayerMovement) { return; }
        
        #if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Shoot();    
        }        
        #endif

        #if UNITY_IOS || UNITY_ANDROID
        if (shoot)
        {
            Shoot();
        }
        #endif
    }

    public void ActivatePowerUp()
    {
        timeToShoot = new WaitForSeconds(shootTime/2);
        Invoke("RemovePowerUp", 3);
    }

    private void RemovePowerUp()
    {
        timeToShoot = new WaitForSeconds(shootTime);
    }

    public void Shoot()
    {
        if(!canShoot) { return; }

        canShoot = false;

        GameObject bulletObj = bulletsPool.GetPooledObject();

        if(bulletObj == null) { return; }
        
        bulletObj.transform.position = shootingPos.transform.position;
        bulletObj.gameObject.SetActive(true);

        StartCoroutine(EnableShooting());
    }

    private IEnumerator EnableShooting()
    {
        yield return timeToShoot;
        canShoot = true;
    }

    // * =====================================================================================================================================
    // * TOUCH

    private bool shoot = false;

    public void OnShootPointerDown()
    {
        shoot = true;
    }

    public void OnShootPointerUp()
    {
        shoot = false;
    }
}
