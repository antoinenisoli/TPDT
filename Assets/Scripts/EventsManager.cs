using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;
    public event Action OnDamagePlayer;
    public event Action OnHealPlayer;
    public event Action OnWin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnAddSanity()
    {
        OnDamagePlayer?.Invoke();
    }

    public void OnRemoveSanity()
    {
        OnHealPlayer?.Invoke();
    }

    public void OnGameWin()
    {
        OnWin?.Invoke();
    }
}
