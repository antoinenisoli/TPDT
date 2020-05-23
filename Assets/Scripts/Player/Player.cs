﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool addition;
    float xInput;
    float yInput;
    [SerializeField] PostProcessVolume volume;
    ChromaticAberration _ChromaticAberration;
    Grain _Grain;

    [Header("Sanity")]    
    public float maxMentalState = 10;
    public float MentalState
    {
        get => mentalState;
        set
        {
            if (value >= maxMentalState)
            {
                value = maxMentalState;
                Reload();
            }

            if (value < 0)
            {
                value = 0;
            }

            mentalState = value;
        }
    }
    [SerializeField] float mentalState;

    [Header("Movements")]
    public float speed = 8;
    public float rotationSpeed = 10;
    public float CellMove;
    public GameObject MenuPause;
    public Transform raycastPos;
    bool paused;
    bool isHit;
    bool isMoving;

    [Header("Detection")]
    public LayerMask wallLayer;
    public LayerMask darkLayer;
    public GameObject LastWall;
    public RaycastHit[] detectedThougts;

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
            ChangeSanity(1, false);
        }
        else if (other.CompareTag("Light") && !addition)
        {
            addition = true;            
            ChangeSanity(-1, true);
        }        
    }

    private void Awake()
    {
        volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out _ChromaticAberration);
        volume.profile.TryGetSettings(out _Grain);
        MenuPause.SetActive(false);
    }

    public void ChangeSanity(float amount, bool loose)
    {
        if (loose)
        {            
            for (int i = 0; i > amount; i--)
            {
                EventsManager.Instance.OnRemoveSanity();
            }
        }
        else 
        {
            for (int i = 0; i < amount; i++)
            {
                EventsManager.Instance.OnAddSanity();
                hitFX.Play();
            }
        }

        if (!isHit)
        {
            MeshRenderer[] renderers = playerMesh.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer render in renderers)
            {
                if (loose)
                {
                    StartCoroutine(Hit(render, Color.green));
                }
                else
                {
                    StartCoroutine(Hit(render, Color.black));
                }
            }
        }

        MentalState += amount;
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

    IEnumerator Hit(MeshRenderer renderer, Color hitColor)
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
            playerMesh.rotation = Quaternion.Euler(0, 270, 0);
        }

        if (xInput > 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (yInput > 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (yInput < 0)
        {
            playerMesh.rotation = Quaternion.Euler(0, 180, 0);
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

    void SanityEffect()
    {
        _ChromaticAberration.intensity.value = Mathf.Lerp(_ChromaticAberration.intensity.value, MentalState / maxMentalState, 3 * Time.deltaTime);
        _Grain.intensity.value = Mathf.Lerp(_Grain.intensity.value, MentalState / maxMentalState, 3 * Time.deltaTime);
    }

    void LineDetection()
    {
        RaycastHit hit;
        bool detectWall = Physics.Raycast(raycastPos.position, playerMesh.forward, out hit, Mathf.Infinity, wallLayer);
        if (detectWall)
        {
            LastWall = hit.collider.gameObject;
            Debug.DrawLine(raycastPos.position, hit.point, Color.red);

            detectedThougts = Physics.RaycastAll(raycastPos.position, playerMesh.forward, Vector3.Distance(transform.position, hit.point), darkLayer);
        }
    }

    private void Update()
    {
        MeshRotation();
        GetInputs();
        SanityEffect();
        LineDetection();
        addition = false;
    }
}
