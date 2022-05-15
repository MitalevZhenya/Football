using System;
using System.Collections.Generic;
using UnityEngine;

public class ReaderAnimationEvents : MonoBehaviour
{
    private Dictionary<string, Action> animationEvents;
    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
    }
    private void RegisterEvents()
    {
        animationEvents = new Dictionary<string, Action>()
        {
            ["ThrowBall"] = InvokeThrowBall
        };
    }
    private void InvokeEvent(string _eventName)
    {
        if(animationEvents.TryGetValue(_eventName, out Action _event))
        {
            print($"{_event.Method.Name}");
            _event?.Invoke();
        }
    }
    private void InvokeThrowBall() => ThrowBallEvent?.Invoke();
    public event Action ThrowBallEvent;
}