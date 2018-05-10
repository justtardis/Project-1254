using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class idMini : MonoBehaviour
{
    public string control = string.Empty;
    public MiniGames mini;
    public int id;
    public Text text;
    public bool opened = false;
    // Use this for initialization
    void triggerAnim()
    {
        if (control == "first") text.text = mini.firstString[id].ToString();
        else if(control == "second") text.text = mini.secondString[id].ToString();
    }
    void Reverse()
    {
        text.text = "?";
    }

    void AnimationEnd()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
