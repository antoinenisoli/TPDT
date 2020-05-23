using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool ReadyForInput;
    Player player;
    public List<VictorySlot> victories;
    bool next;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void FindVictories()
    {
        if (victories.Count == 0 && !next)
        {
            EventsManager.Instance.OnGameWin();
            next = true;
        }
    }

    void Movements()
    {
        Vector3 MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        MoveInput.Normalize();
        if (MoveInput.sqrMagnitude >= 0.5)
        {
            if (ReadyForInput)
            {
                ReadyForInput = false;
                player.Move(MoveInput);
            }
        }
        else
        {
            ReadyForInput = true;
        }
    }

    void Update()
    {
        FindVictories();
        Movements();        
    }
}
