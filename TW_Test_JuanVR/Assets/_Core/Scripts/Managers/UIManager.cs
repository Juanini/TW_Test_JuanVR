using GameEventSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;

    public List<LifeUI> lifeList;

    void Awake()
    {
        Ins = this;
        SetupEvents();    
    }

    void OnDestroy() 
    {
        DestroyEvents();    
    }

    // * =====================================================================================================================================
    // * LIFES

    private void OnPlayerDamage(Hashtable _params)
    {
        RemoveLife();
    }

    public void AddLife()
    {
        for (int i = 0; i < lifeList.Count; i++)
        {
            if(!lifeList[i].isOn)
            {
                lifeList[i].SetLifeOn();
                break;
            }
        }
    }

    public void RemoveLife()
    {
        for (int i = lifeList.Count - 1; i >= 0; i--)
        {
            if(lifeList[i].isOn)
            {
                lifeList[i].SetLifeOff();
                break;
            }
        }
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
