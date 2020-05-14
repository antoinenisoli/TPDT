using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool addition;
    GameManager gmScript;
    float xInput;
    float yInput;

    [Header("Movements")]
    public float speed = 8;
    public float rotationSpeed = 10;
    public float CellMove;
    public GameObject MenuPause;
    bool paused;
    bool isHit;
    bool isMoving;

    [Header("Meshes")]
    public float colorTransitionSpeed = 10;
    public float delay = 0.2f;
    public Color hitColor = Color.red;
    Color baseColor;
    public Transform playerMesh;

    [Header("FX")]
    public ParticleSystem hitFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DT") && !addition)
        {
            addition = true;
            hitFX.Play();

            if (!isHit)
            {
                MeshRenderer[] renderers = playerMesh.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer render in renderers)
                {
                    StartCoroutine(Hit(render));
                }
            }
            
            gmScript.MentalState++;
        }
    }

    private void Start()
    {
        MenuPause.SetActive(false);
        gmScript = FindObjectOfType<GameManager>();
    }

    public bool Move(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5f)
        {
            direction.x = 0;
        }
        else
        {
            direction.z = 0;
        }

        direction.Normalize();

        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            if (!paused && !isMoving)
            {
                StartCoroutine(SmoothTranslation(transform.position + (direction * CellMove)));
            }

            return true;
        }
    }

    IEnumerator Hit(MeshRenderer renderer)
    {
        baseColor = renderer.material.color;
        isHit = true;
        float counter = 0;

        while (counter < delay)
        {
            yield return null;
            counter += Time.deltaTime;
            renderer.material.color = Color.Lerp(renderer.material.color, hitColor, colorTransitionSpeed * Time.deltaTime);
        }

        counter = 0;

        while (counter < delay)
        {
            yield return null;
            counter += Time.deltaTime;
            renderer.material.color = Color.Lerp(renderer.material.color, baseColor, colorTransitionSpeed * Time.deltaTime);
        }

        isHit = false;
    }

    IEnumerator SmoothTranslation(Vector3 dir)
    {
        isMoving = true;
        while (transform.position != dir)
        {
            yield return null;
            dir.y = 0;
            transform.position = Vector3.MoveTowards(transform.position, dir, speed * Time.deltaTime);
        }
        isMoving = false;
    }
    
    bool Blocked(Vector3 position, Vector3 direction)
    {
        Vector3 newpos = new Vector3(position.x, position.y, position.z) + direction* CellMove;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls){
                
            if (wall.transform.position.x == newpos.x && wall.transform.position.z == newpos.z)
            {
                return true;
            }
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            if (box.transform.position.x == newpos.x && box.transform.position.z == newpos.z)
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

    public void Pause()
    {        
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        MenuPause.SetActive(Time.timeScale == 0);
        paused = MenuPause.activeSelf;
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);        
        paused = false;
        Time.timeScale = 1;
        MenuPause.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MeshRotation()
    {
        if (xInput < 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (xInput > 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, -90, 0);
        }

        if (yInput > 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (yInput < 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void GetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Update()
    {
        MeshRotation();
        GetInputs();
        addition = false;
    }
}
