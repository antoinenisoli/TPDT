using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityUI : MonoBehaviour
{
    public Transform aiguille;
    Player player;
    Vector3 newRotation;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        EventsManager.Instance.OnDamagePlayer += RotateClock;
        newRotation = aiguille.eulerAngles;
    }

    private void RotateClock()
    {
        newRotation += new Vector3(0, 0, 360 / player.maxMentalState - 1);
    }

    private void Update()
    {
        aiguille.localEulerAngles = Vector3.Lerp(aiguille.localEulerAngles, newRotation, Time.deltaTime);
    }
}
