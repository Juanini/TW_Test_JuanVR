using DG.Tweening;
using TMPro;
using GameEventSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Ins;

    [BoxGroup("UI")]
    public Button pauseButton; 

    [BoxGroup("Lifes")]
    public GameObject lifeContainer;
    [BoxGroup("Lifes")]
    public List<LifeUI> lifeList;

    [BoxGroup("Score")]
    public GameObject scoreContainer;
    [BoxGroup("Score")]
    public TextMeshProUGUI scoreText;

    [BoxGroup("UX")]
    public TextMeshProUGUI levelCounterText;

    [BoxGroup("Touch Controls")]
    public GameObject touchControls;
    [BoxGroup("Touch Controls")]
    public GameObject joystick;

    private Dictionary<int, string> menuDict;

    void Awake()
    {
        Ins = this;
        SetupEvents();    
        SetupMenus();

        pauseButton.onClick.AddListener(OnPauseClick);
    }

    void OnDestroy() 
    {
        DestroyEvents();    
    }

    // * =====================================================================================================================================
    // * 

    private void SetupMenus()
    {
        menuDict = new Dictionary<int, string>();
        menuDict.Add(GameConstants.MENU_MAIN,       "MainMenu");
        menuDict.Add(GameConstants.MENU_CREDITS,    "CreaditsMenu");
        menuDict.Add(GameConstants.MENU_END_GAME,   "EndMenu");
        menuDict.Add(GameConstants.MENU_PAUSE,      "PauseMenu");
        menuDict.Add(GameConstants.MENU_COMPLETE,   "LevelCompleteMenu");

        MenuManager.Ins.CreateMenuDic(menuDict);
    }

    // * =====================================================================================================================================
    // * SCORE

    Tween scoreTween;

    public void AddScore(int _score)
    {
        
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
    // * LEVEL COUNTER

    public void LayoutSelected(Hashtable _ht)
    {
        scoreContainer.gameObject.SetActive(true);
        lifeContainer.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);

        #if !UNITY_STANDALONE
        ShowTouchControls();
        #endif

        DoLevelCounter();
        scoreText.gameObject.SetActive(true);
    }

    public void ShowTouchControls()
    {
        touchControls.gameObject.SetActive(true);
        joystick.gameObject.SetActive(true);
    }

    private int levelCounteriter = 3;

    private void DoLevelCounter()
    {
        if (levelCounteriter > 0)
        {
            levelCounterText.gameObject.SetActive(true);
            
            levelCounterText.text = levelCounteriter.ToString();
            levelCounterText.transform.DOPunchScale(new Vector3(0.35f, 0.35f, 0.35f), 0.4f);

            levelCounteriter--;    
            Invoke("DoLevelCounter", 1);
        }
        else
        {
            levelCounterText.text = "GO!";
            levelCounterText.transform.DOPunchScale(new Vector3(0.35f, 0.35f, 0.35f), 0.4f);
            
            Invoke("HideCounter", 1);

            GameEventManager.TriggerEvent(GameEvents.ON_LEVEL_COUNTER_END);
        }
    }

    private void HideCounter()
    {
        levelCounterText.gameObject.SetActive(false);
    }

    // * =====================================================================================================================================
    // * GAME OVER

    private bool gameOverShown = false;

    public void OnGameOver(Hashtable _ht)
    {
        if(gameOverShown) { return; }
        gameOverShown = true;
        MenuManager.Ins.ShowMenu(GameConstants.MENU_END_GAME);
    }

    // * =====================================================================================================================================
    // * PAUSE

    public void OnPauseClick()
    {
        MenuManager.Ins.ShowMenu(GameConstants.MENU_PAUSE);
    }

    // * =====================================================================================================================================
    // * Events

    private void SetupEvents()
    {
        GameEventManager.StartListening(GameEvents.ON_PLAYER_DAMAGE, OnPlayerDamage);
        GameEventManager.StartListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StartListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);

    }

    private void DestroyEvents()
    {
        GameEventManager.StopListening(GameEvents.ON_PLAYER_DAMAGE, OnPlayerDamage);
        GameEventManager.StopListening(GameEvents.ON_GAME_OVER, OnGameOver);
        GameEventManager.StopListening(GameEvents.ON_LAYOUT_SELECT, LayoutSelected);
    }
}
