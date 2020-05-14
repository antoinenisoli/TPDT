using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool Rblocked;
    public bool Lblocked;
    public bool Tblocked;
    public bool Dblocked;

    public GameObject leftCollider;
    public GameObject rightCollider;
    public GameObject topCollider;
    public GameObject downCollider;

    public GameObject[] walls;
    public GameObject[] boxes;

    public float portee;
    public float distance;

    public LayerMask wallLayer;

    Vector2 left;
    Vector2 right;
    Vector2 top;
    Vector2 down;

    public Transform origin;
    Vector2 originVector;

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Lblocked) //gauche
        {
            transform.position += new Vector3(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !Rblocked) //droite
        {
            transform.position += new Vector3(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !Tblocked) //haut
        {
            transform.position += new Vector3(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !Dblocked) //bas
        {
            transform.position += new Vector3(0, -1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawLine(originVector, left);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(originVector, right);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(originVector, top);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(originVector, down);*/
    }

    void FixedUpdate()
    {
        originVector = new Vector2(origin.position.x, origin.position.y);

        left = new Vector2(origin.position.x - portee, origin.position.y);
        right = new Vector2(origin.position.x + 1, origin.position.y);
        top = new Vector2(origin.position.x, origin.position.y + 1);
        down = new Vector2(origin.position.x, origin.position.y - 1);

        //Lblocked = Physics2D.Raycast(originVector, left, distance, wallLayer);
    }

    void WallsDetect()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject wall in walls)
        {
            if (leftCollider.transform.position == wall.transform.position)
            {
                Lblocked = true;
            }
            else
            {
                Lblocked = false;
            }

            if (rightCollider.transform.position == wall.transform.position)
            {
                Rblocked = true;
            }
            else
            {
                Rblocked = false;
            }

            if (topCollider.transform.position == wall.transform.position)
            {
                Tblocked = true;
            }
            else
            {
                Tblocked = false;
            }

            if (downCollider.transform.position == wall.transform.position)
            {
                Dblocked = true;
            }
            else
            {
                Dblocked = false;
            }
        }
    }

    void BoxesMove()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (var box in boxes)
        {
            if (transform.position == box.transform.position)
            {
                Lblocked = true;
            }
            else
            {
                Lblocked = false;
            }

            
        }
    }

    bool Blocked()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (var wall in walls)
        {
            if (leftCollider.transform.position == wall.transform.position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void Update()
    {
        Move();
        WallsDetect();
        BoxesMove();
    }
}
