using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    protected Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public virtual void Effect()
    {
        Destroy(gameObject);
    }

    public virtual bool CanUse()
    {
        return true;
    }
}
