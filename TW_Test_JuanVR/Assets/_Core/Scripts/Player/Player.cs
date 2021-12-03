using GameEventSystem;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Sprite hitSprite;
    public Sprite normalSprite;

    private int lifes = 5;
    private float immuneTime = 2f;
    private bool isImmune = false;

    private SpriteRenderer spriteRender;

    private WaitForSeconds immuneTimeWait;
    private WaitForSeconds hitSpriteWait;

    void Awake() 
    {
        InitComponents();
        immuneTimeWait = new WaitForSeconds(immuneTime);
        hitSpriteWait = new WaitForSeconds(0.2f);
    }

    private void InitComponents()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // * =====================================================================================================================================
    // * Damage

    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void OnDamage(int _damage)
    {
        if(isImmune) { return; }
        
        isImmune = true;

        spriteRender.sprite = hitSprite;
        StartCoroutine(RemoveSpriteHit());
        StartCoroutine(RemoveImmune());

        GameEventManager.TriggerEvent(GameEvents.ON_PLAYER_DAMAGE);
    }

    private IEnumerator RemoveImmune()
    {
        yield return immuneTimeWait;
        isImmune = false;

        CancelInvoke("SpriteBlink");
        spriteRender.enabled = true;
    }

    private IEnumerator RemoveSpriteHit()
    {
        yield return hitSpriteWait;
        spriteRender.sprite = normalSprite;
        InvokeRepeating("SpriteBlink", 0, 0.2f);
    }

    private void SpriteBlink() => spriteRender.enabled = !spriteRender.enabled;
    
}
