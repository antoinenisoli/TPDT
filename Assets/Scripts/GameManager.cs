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

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        print("next !");
        if ((SceneManager.GetActiveScene().buildIndex + 1) <= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);            
        }
    }

    public void FindVictories()
    {
        if (victories.Count == 0 && !next)
        {
            StartCoroutine(NextLevel());
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
