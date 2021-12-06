using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    public Button closeButton;

    void Start() 
    {
        closeButton.onClick.AddListener(CloseCreditsMenu);
    }

    private void CloseCreditsMenu()
    {
        MenuManager.Ins.ShowMenu(GameConstants.MENU_MAIN);
    }
}
