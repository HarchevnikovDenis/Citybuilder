using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        InitSingleton();
    }

    private void InitSingleton()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            if (Instance != (T)this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Add()
    {

    }
}
