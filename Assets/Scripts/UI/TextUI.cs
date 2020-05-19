using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextUI : MonoBehaviour
{
    Text UItext;
    public string txt = "Mental state : ";
    public Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        UItext = GetComponent<Text>();
    }

    void Update()
    {
        string texte = (txt + player.MentalState);
        UItext.text = texte;
    }
}
