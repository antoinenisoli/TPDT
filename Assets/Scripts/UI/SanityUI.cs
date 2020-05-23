using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityUI : MonoBehaviour
{
    public Transform aiguille;
    Player player;
    public Vector3 newRotation;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        aiguille.localEulerAngles = new Vector3(0, 0, (player.maxMentalState - player.MentalState) * 35.5f);
    }

    private void Start()
    {
        newRotation = aiguille.eulerAngles;
    }

    private void Update()
    {
        newRotation = new Vector3(0, 0, (player.maxMentalState - player.MentalState) * 35.5f);
        aiguille.localEulerAngles = Vector3.Lerp(aiguille.localEulerAngles, newRotation, Time.deltaTime);
    }
}
