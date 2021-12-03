using GameEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Ins;

    public Camera cam;
    public ProCamera2DShake camShake;

    void Awake()
    {
        Ins = this;
        SetupEvents();
    }

    public void DoShake()
    {
        camShake.Shake(0);
    }

    void OnDestroy() 
    {
        DestroyEvents();    
    }

    private void OnPlayerDamage(Hashtable _params)
    {
        DoShake();
    }

    // * =====================================================================================================================================
    // * Events

    private void SetupEvents()
    {
        GameEventManager.StartListening(GameEvents.ON_PLAYER_DAMAGE, OnPlayerDamage);
    }

    private void DestroyEvents()
    {
        GameEventManager.StopListening(GameEvents.ON_PLAYER_DAMAGE, OnPlayerDamage);

    }
}
