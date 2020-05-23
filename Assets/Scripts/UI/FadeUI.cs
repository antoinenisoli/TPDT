using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeUI : MonoBehaviour
{
    Animator anim;
    public float TransitionDelay = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("FadeIn", false);
        anim.SetBool("FadeOut", true);
        EventsManager.Instance.OnWin += NextLevel;
    }

    public void NextLevel()
    {
        StartCoroutine(StartFading());
    }

    public void LoadNextLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) <= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public IEnumerator StartFading()
    {
        yield return new WaitForSeconds(TransitionDelay);
        anim.SetBool("FadeIn", true);
        anim.SetBool("FadeOut", false);        
    }
}
