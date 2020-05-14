using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public bool Move(Vector3 direction)
    {        
        if (BoxBlocked(transform.position, direction))
        {            
            return false;           
        }
        else
        {
            StartCoroutine(SmoothTranslation(transform.position + direction));
            return true;
        }
    }

    IEnumerator SmoothTranslation(Vector3 dir)
    {
        while (transform.position != dir)
        {
            yield return null;
            dir.y = 0;
            transform.position = Vector3.MoveTowards(transform.position, dir, player.speed * Time.deltaTime);
        }
    }

    bool BoxBlocked(Vector3 position, Vector3 direction)
    {
        Vector3 newpos = new Vector3(position.x, position.y, position.z) + direction ;
        
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {          
            if (wall.transform.position == newpos)
            {                
                return true;
            }
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            if (box.transform.position == newpos)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        return false;
    }
}
