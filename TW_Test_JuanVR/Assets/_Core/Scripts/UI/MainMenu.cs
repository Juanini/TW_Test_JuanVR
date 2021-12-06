using GameEventSystem;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuBase
{
    public Button layout1Button;
    public Button layout2Button;
    public Button creditsButton;

    private int layoutSelected = 0;

    void Start()
    {
        SetupButtons();     
    }

    public void SetupButtons()
    {
        layout1Button.onClick.AddListener(() => SelectLayout(GameConstants.LAYOUT_1));
        layout2Button.onClick.AddListener(() => SelectLayout(GameConstants.LAYOUT_2));
        creditsButton.onClick.AddListener(OnCreditsClick);
    }

    private void SelectLayout(int _layout)
    {
        layoutSelected = _layout;
        FadePanel.Ins.FadeIn(FadeComplete);
    }

    private void FadeComplete()
    {
        Hashtable ht = new Hashtable();
        ht.Add(GameEventParam.LAYOUT_SELECTED, layoutSelected);
        GameEventManager.TriggerEvent(GameEvents.ON_LAYOUT_SELECT, ht);
        onExitScreen();
    }

    private void OnCreditsClick()
    {
        MenuManager.Ins.ShowMenu(GameConstants.MENU_CREDITS);
    }
}
