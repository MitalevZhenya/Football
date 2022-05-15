using System;
using UnityEngine;

public class RegisterCollision : MonoBehaviour
{
    private bool isTrigger;
    private void Start()
    {
        isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;
        print($"{other.name}");
        if (other.TryGetComponent<BaseObstacles>(out BaseObstacles _obstacle))
        {
            print($"{other.name} obstacle");
            OnCollisionEvent?.Invoke(_obstacle);
            isTrigger = false;
        }
        isTrigger = true;
    }
    public event Action<BaseObstacles> OnCollisionEvent;
}