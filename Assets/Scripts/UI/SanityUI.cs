using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityUI : MonoBehaviour
{
    public GameObject aiguille;
    Player player;
    Vector3 rotation;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        EventsManager.Instance.OnDamagePlayer += RotateClock;
    }

    private void RotateClock()
    {
        rotation = new Vector3(0, 0, -360/player.maxMentalState);
        aiguille.transform.Rotate(rotation);
    }
}
