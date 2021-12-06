using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadePanel : MonoBehaviour
{
    public static FadePanel Ins;

    public Image fadeImage;

    private UnityAction callback;

    private void Awake() 
    {
        Ins = this;    
    }

    public void SetBack()
    {
        fadeImage.color = new Color(0, 0, 0, 1);
    }

    public void FadeIn(UnityAction _callback = null, float _fadeTime = 0.65f)
    {
        callback = _callback;
        fadeImage.DOFade(1, _fadeTime).OnComplete(()=> callback?.Invoke());
    }

    public void FadeOut(UnityAction _callback = null, float _fadeTime = 0.65f)
    {
        callback = _callback;
        fadeImage.DOFade(0, _fadeTime).OnComplete(()=> callback?.Invoke());
    }
}
