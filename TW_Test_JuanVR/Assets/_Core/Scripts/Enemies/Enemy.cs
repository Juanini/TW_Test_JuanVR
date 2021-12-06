using GameEventSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public GameObject shootingPos;

    private SpriteRenderer spriteRender;
    private Animator anim;
    private BoxCollider2D boxCollider2D;

    private int lifes = 2;
    private int points = 0;
    private int enemyType;

    private WaitForSeconds hitWait;
    
    private bool isImmune = false;
    private bool isDead = false;

    public int posInList = -1;

    void Awake() 
    {
        InitComponents();    
        hitWait = new WaitForSeconds(0.15f);
    }

    // * =====================================================================================================================================
    // * SETUP

    public void SetupEnemy(EnemyData _enemyData)
    {
        enemyData = _enemyData;

        spriteRender.sprite = enemyData.enemySprite;
        lifes       = enemyData.enemyLifes;
        points      = enemyData.enemyPoints;
        enemyType   = (int)enemyData.enemyType;
    }

    private void InitComponents()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void Shoot()
    {
        GameObject bullet = EnemiesManager.Ins.bulletPool.GetPooledObject();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootingPos.transform.position;
    }

    // * =====================================================================================================================================
    // * DAMAGE

    private void OnTriggerEnter2D(Collider2D _other) 
    {
        if(_other.tag == GameConstants.TAG_BULLET) 
        { 
            OnDamage();
        }

        if(_other.tag == GameConstants.TAG_GAME_OVER_COLLIDER) 
        { 
            Trace.Log(this.name + " - " + "GAME OVER");
            GameEventManager.TriggerEvent(GameEvents.ON_GAME_OVER);
        }
    }

    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void OnDamage()
    {
        if(isImmune) { return; }
        if(isDead) { return; }

        isImmune = true;
        Trace.Log(this.name + " - " + "Enemy Collision " + " OnDAMAFE");
        spriteRender.material.SetFloat("_HitEffectBlend", 1);
        StartCoroutine(RemoveHit());
    }

    private IEnumerator RemoveHit()
    {
        yield return hitWait;
        spriteRender.material.SetFloat("_HitEffectBlend", 0);

        CheckLifes();
    }

    private void CheckLifes()
    {
        lifes--;

        if (lifes <= 0)
        {
            EnemyDead();
        }
        else
        {
            transform.DOShakePosition(0.15f, 0.065f, 40);
            StartCoroutine(RemoveImmune());
        }
    }

    private IEnumerator RemoveImmune()
    {
        yield return new WaitForSeconds(0.20f);
        isImmune = false;
    }

    public void DeadAnimComplete()
    {
        gameObject.SetActive(false);
    }

    private void EnemyDead()
    {
        boxCollider2D.enabled = false;
        isDead = true;
        anim.enabled = true;
        anim.Play("Dead");

        Hashtable ht = new Hashtable();
        ht.Add(GameEventParam.ENEMY_POS, transform.position);
        ht.Add(GameEventParam.ENEMY_POINTS, points);
        ht.Add(GameEventParam.ENEMY_REF, this);
        GameEventManager.TriggerEvent(GameEvents.ON_ENEMY_DEAD, ht);
    }
}
