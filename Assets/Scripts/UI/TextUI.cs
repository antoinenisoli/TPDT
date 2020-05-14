using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextUI : MonoBehaviour
{
    Text UItext;
    public string txt = "Mental state : ";
    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        UItext = GetComponent<Text>();
    }

    void Update()
    {
        string texte = (txt + gm.MentalState);
        UItext.text = texte;
    }
}
