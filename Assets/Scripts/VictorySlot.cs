using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VictorySlot : MonoBehaviour
{
    GameManager gm;
    bool Win;
    bool addition;

    [Header("Lights")]
    public GameObject greenlight;
    public GameObject redlight;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Win = true;
            gm.victories.Remove(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Win = false;
            gm.victories.Add(this);
        }
    }

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        
        if (gm != null)
        {
            gm.victories.Add(this);
        }
    }

    void Lights()
    {
        greenlight.SetActive(Win);
        redlight.SetActive(!Win);
    }

    void Update()
    {
        Lights();
    }
}
