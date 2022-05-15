using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<T>();
            return instance;
        }
    }

    protected void Awake()
    {
        if (instance == null)
            instance = this as T;
        else if (instance != this)
            DestroySelf();
    }

    public void DestroySelf()
    {
        if (Application.isPlaying)
            Destroy(this);
        else
            DestroyImmediate(this);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}