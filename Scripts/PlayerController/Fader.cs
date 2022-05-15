using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float duraction, invokeFade;
    private Tween fadeTween;
    private void Start()
    {
        EndFade();
    }
    public void StartFade(Action _endFade)
    {
        StartCoroutine(InvokeFade(_endFade));
    }
    private IEnumerator InvokeFade(Action _endFade)
    {
        yield return new WaitForSeconds(invokeFade);
        Fade(1, () =>
        {
            _endFade?.Invoke();
            EndFade();
        });
    }
    private void EndFade()
    {
        Fade(0, () => { });
    }
    private void Fade(float _value, TweenCallback _endAction)
    {
        if(fadeTween != null)
        {
            fadeTween.Kill(false);
        }
        fadeTween = fadePanel.DOFade(_value, duraction);
        if(_endAction != null)
            fadeTween.onComplete += _endAction;
    }
}