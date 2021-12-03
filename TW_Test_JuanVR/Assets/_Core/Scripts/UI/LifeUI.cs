using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public Image lifeImage;
    public Sprite lifeOn;
    public Sprite lifeOff;

    public bool isOn;

    void Start() 
    {
        SetLifeOn();
    }

    public void SetLifeOn()
    {   
        isOn = true;
        lifeImage.sprite = lifeOn;
    }

    public void SetLifeOff()
    {
        isOn = false;
        lifeImage.sprite = lifeOff;
    }
}
